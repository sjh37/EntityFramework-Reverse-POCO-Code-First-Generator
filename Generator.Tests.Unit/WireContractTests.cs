using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Efrpg.Readers;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    /// <summary>
    ///     The efrpg tool and this generator each carry a hand-maintained copy of every type that crosses the wire
    ///     between them. They live in separate repositories, so no build sees both halves, and the failure mode is
    ///     not a compiler error: a member added to one copy alone is simply never populated, which reads as a
    ///     database that does not report that feature.
    /// </summary>
    /// <remarks>
    ///     These replace ParallelSourceTests, which diffed the two copies of the C# and therefore needed both on
    ///     disk. The contract was never the source anyway - it is the XML, as the "Wire format contract" section
    ///     of AGENTS.md says - so these tests check a captured tool payload instead.
    ///
    ///     What is deliberately NOT asserted: that every member holds a non-default value. That would demand the
    ///     test database exercise every feature the wire format can express, which no database does, and the test
    ///     would fail for reasons that have nothing to do with drift. Name presence is the honest check: if the
    ///     tool emits an attribute, the generator can read it.
    ///
    ///     Three gaps compared with the old source diff, recorded so nobody assumes coverage that is not here.
    ///     The language mapping keys are no longer compared at all: an unknown key deliberately falls through to
    ///     each mapping's default - that is how every SQL Server character type is typed - so "the key is missing"
    ///     cannot be asserted mechanically. MinMaxValueCache is not compared either, because it never crosses the
    ///     wire; both sides hold a copy but only the generator's affects generated output. And because the payload
    ///     is searched for a name rather than for a name on a particular element, a member can be matched by an
    ///     attribute of the same name belonging to a different DTO.
    /// </remarks>
    [TestFixture]
    [Category(Constants.CI)]
    public class WireContractTests
    {
        /// <summary>
        ///     Members computed on the generator side rather than read from the payload. Every entry is a member
        ///     nothing is checking, so keep this list as short as the truth allows.
        /// </summary>
        private static readonly Dictionary<string, string> NotCarriedOnTheWire = new Dictionary<string, string>
        {
            { "EfrpgResult.HasErrors",                       "Derived from Errors." },
            { "RawExtendedProperty.TableLevelExtendedComment","Derived from columnName being empty." },
            { "RawSequence.hasMinValue",                     "Computed from MinMaxValueCache and the sent minValue." },
            { "RawSequence.hasMaxValue",                     "Computed from MinMaxValueCache and the sent maxValue." },
            { "RawEnumRow.AllValues",                        "Carried as repeated Field elements, not an attribute of the same name." },
            { "MultiContextStoredProcedureSettings.ReturnModel", "No context in EfrpgTest_Settings defines a stored procedure, so the tool has nothing to emit it from. Unchecked until that database grows one." }
        };

        private static string FixturePath(string filename)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WireContract", filename);
        }

        private static XElement Fixture(string filename)
        {
            return XDocument.Parse(File.ReadAllText(FixturePath(filename))).Root;
        }

        [Test]
        public void EfrpgResult_EveryWireMemberIsNamedInThePayload()
        {
            var missing = MembersNotNamedIn(Fixture("EfrpgResult.xml"), typeof(EfrpgResult));

            Assert.That(missing, Is.Empty, Explain(missing, "EfrpgResult.xml"));
        }

        [Test]
        public void EnumData_EveryWireMemberIsNamedInThePayload()
        {
            var missing = MembersNotNamedIn(Fixture("EnumData.xml"), typeof(RawEnum));

            Assert.That(missing, Is.Empty, Explain(missing, "EnumData.xml"));
        }

        /// <summary>
        ///     The reader rejects a payload below its schema floor, so this fails if the fixture predates a
        ///     RequiredSchemaVersion bump and is therefore no longer representative of what a current tool sends.
        /// </summary>
        [Test]
        public void EfrpgResult_FixtureIsAcceptedByTheReader()
        {
            Assert.That(() => EfrpgResultXmlReader.Read(File.ReadAllText(FixturePath("EfrpgResult.xml"))), Throws.Nothing);
        }

        /// <summary>
        ///     Guards against the fixture rotting into a well-formed but empty document, which would make every
        ///     other test in here pass for the wrong reason.
        /// </summary>
        [Test]
        public void EfrpgResult_FixtureCarriesRowsForEveryCollection()
        {
            var result = EfrpgResultXmlReader.Read(File.ReadAllText(FixturePath("EfrpgResult.xml")));

            Assert.Multiple(() =>
            {
                Assert.That(result.Tables,                Is.Not.Empty, "Tables");
                Assert.That(result.ForeignKeys,           Is.Not.Empty, "ForeignKeys");
                Assert.That(result.Indexes,               Is.Not.Empty, "Indexes");
                Assert.That(result.StoredProcedures,      Is.Not.Empty, "StoredProcedures");
                Assert.That(result.Sequences,             Is.Not.Empty, "Sequences");
                Assert.That(result.Triggers,              Is.Not.Empty, "Triggers");
                Assert.That(result.MemoryOptimisedTables, Is.Not.Empty, "MemoryOptimisedTables");
                Assert.That(result.ExtendedProperties,    Is.Not.Empty, "ExtendedProperties");
                Assert.That(result.MultiContextSettings,  Is.Not.Empty, "MultiContextSettings");
                Assert.That(result.Errors,                Is.Not.Empty, "Errors");
            });
        }

        private static string Explain(IReadOnlyCollection<string> missing, string fixtureName)
        {
            return "These wire members have no matching attribute or element in " + fixtureName + "." +
                   Environment.NewLine +
                   "Either the fixture is stale, or the tool never emits them:" + Environment.NewLine +
                   string.Join(Environment.NewLine, missing);
        }

        /// <summary>
        ///     Walks the DTO type graph statically - not an instance graph, so a type reachable only through a
        ///     collection the fixture happens to leave empty is still checked - and returns every member with no
        ///     corresponding name in the payload.
        /// </summary>
        private static List<string> MembersNotNamedIn(XElement root, Type rootType)
        {
            var attributeNames = new HashSet<string>(StringComparer.Ordinal);
            var elementNames = new HashSet<string>(StringComparer.Ordinal);
            CollectNames(root, attributeNames, elementNames);

            var missing = new List<string>();
            var visitedTypes = new HashSet<Type>();

            void Visit(Type type)
            {
                if (!IsWireType(type) || !visitedTypes.Add(type))
                    return;

                foreach (var member in WireMembers(type))
                {
                    var qualified = type.Name + "." + member.Name;
                    if (!NotCarriedOnTheWire.ContainsKey(qualified))
                    {
                        if (!attributeNames.Contains(CamelCase(member.Name)) && !MatchesElement(elementNames, member.Name))
                            missing.Add(qualified);
                    }

                    Visit(ElementType(member.MemberType));
                }
            }

            Visit(rootType);

            return missing.Distinct().OrderBy(x => x).ToList();
        }

        /// <summary>
        ///     A trailing "s" is allowed to differ in either direction: RawEnum.Rows holds Row elements, and
        ///     RawSequence.TableMapping is carried inside TableMappings.
        /// </summary>
        private static bool MatchesElement(ICollection<string> elementNames, string memberName)
        {
            if (elementNames.Contains(memberName) || elementNames.Contains(memberName + "s"))
                return true;

            return memberName.EndsWith("s", StringComparison.Ordinal) &&
                   elementNames.Contains(memberName.Substring(0, memberName.Length - 1));
        }

        private static void CollectNames(XElement element, ISet<string> attributeNames, ISet<string> elementNames)
        {
            elementNames.Add(element.Name.LocalName);

            foreach (var attribute in element.Attributes())
                attributeNames.Add(attribute.Name.LocalName);

            foreach (var child in element.Elements())
                CollectNames(child, attributeNames, elementNames);
        }

        private static string CamelCase(string name)
        {
            return string.IsNullOrEmpty(name) ? name : char.ToLowerInvariant(name[0]) + name.Substring(1);
        }

        /// <summary>
        ///     Unwraps List&lt;T&gt; and List&lt;List&lt;T&gt;&gt; down to the type actually carried.
        /// </summary>
        private static Type ElementType(Type type)
        {
            while (type != null && type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type))
            {
                if (!type.IsGenericType)
                    return null;

                type = type.GetGenericArguments().Last();
            }

            return type;
        }

        private static bool IsWireType(Type type)
        {
            return type != null &&
                   !type.IsEnum &&
                   !type.IsPrimitive &&
                   type != typeof(string) &&
                   type.Namespace != null &&
                   type.Namespace.StartsWith("Efrpg", StringComparison.Ordinal);
        }

        private static IEnumerable<WireMember> WireMembers(Type type)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            foreach (var field in type.GetFields(flags))
                yield return new WireMember(field.Name, field.FieldType);

            foreach (var property in type.GetProperties(flags).Where(p => p.CanRead && p.GetIndexParameters().Length == 0))
                yield return new WireMember(property.Name, property.PropertyType);
        }

        private class WireMember
        {
            public string Name { get; }
            public Type MemberType { get; }

            public WireMember(string name, Type memberType)
            {
                Name = name;
                MemberType = memberType;
            }
        }
    }
}
