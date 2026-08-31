using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     The hand-written JSON reader, checked against System.Text.Json on the only two documents it will ever
    ///     see.
    /// </summary>
    /// <remarks>
    ///     Hand-writing a parser is only defensible if it is held to a real standard. This project cannot take a
    ///     serialiser dependency into the Visual Studio process, so instead both shipped metadata files are parsed
    ///     twice - once with <see cref="Json"/>, once with the framework - and every member is compared.
    /// </remarks>
    [TestFixture]
    public class JsonTests
    {
        [TestCase("v3")]
        [TestCase("v4")]
        public void TheShippedMetadataParsesIdenticallyToSystemTextJson(string version)
        {
            var text = RepositoryFiles.SettingsMetadata(version);

            var mine = Flatten(Json.Parse(text), "$").OrderBy(p => p.Key, StringComparer.Ordinal).ToList();
            using var document = JsonDocument.Parse(text);
            var theirs = Flatten(document.RootElement, "$").OrderBy(p => p.Key, StringComparer.Ordinal).ToList();

            Assert.That(mine.Count, Is.EqualTo(theirs.Count), "different number of values");
            Assert.That(mine, Is.EqualTo(theirs));
        }

        [TestCase(@"""hello""", "hello")]
        [TestCase(@"""a\""b""", "a\"b")]
        [TestCase(@"""a\\b""", @"a\b")]
        [TestCase(@"""a\nb""", "a\nb")]
        [TestCase(@"""aAb""", "aAb")]
        [TestCase(@"""a\/b""", "a/b")]
        public void StringsAreUnescapedTheSameWayJsonBuilderEscapedThem(string json, string expected)
        {
            Assert.That(Json.Parse(json).AsString, Is.EqualTo(expected));
        }

        [Test]
        public void MembersAreReachableByName()
        {
            var value = Json.Parse(@"{ ""a"": 1, ""b"": { ""c"": true }, ""d"": [1, 2, 3] }");

            Assert.That(value["a"].AsInteger, Is.EqualTo(1));
            Assert.That(value["b"]["c"].AsBoolean, Is.True);
            Assert.That(value["d"].Items.Count, Is.EqualTo(3));
            Assert.That(value["nope"], Is.Null);
        }

        [Test]
        public void EmptyObjectsAndArraysParse()
        {
            Assert.That(Json.Parse("{}")["x"], Is.Null);
            Assert.That(Json.Parse("[]").Items, Is.Empty);
        }

        [Test]
        public void NullIsDistinctFromAbsent()
        {
            var value = Json.Parse(@"{ ""a"": null }");

            Assert.That(value["a"], Is.Not.Null);
            Assert.That(value["a"].IsNull, Is.True);
            Assert.That(value["a"].AsString, Is.Null);
        }

        /// <summary>
        ///     Malformed input must fail loudly. A parser that silently returns an empty document would leave the
        ///     editor showing no settings at all with no explanation.
        /// </summary>
        [TestCase("")]
        [TestCase("{")]
        [TestCase(@"{ ""a"" 1 }")]
        [TestCase(@"{ ""a"": }")]
        [TestCase(@"""unterminated")]
        [TestCase("tru")]
        [TestCase("{} extra")]
        public void MalformedJsonThrows(string json)
        {
            Assert.That(() => Json.Parse(json), Throws.TypeOf<FormatException>());
        }

        private static IEnumerable<KeyValuePair<string, string>> Flatten(Json value, string path)
        {
            if (value.IsNull)
            {
                yield return new KeyValuePair<string, string>(path, "null");
                yield break;
            }

            if (value.AsString != null)
            {
                yield return new KeyValuePair<string, string>(path, "s:" + value.AsString);
                yield break;
            }

            var items = value.Items;
            if (items.Count > 0)
            {
                for (var i = 0; i < items.Count; i++)
                    foreach (var pair in Flatten(items[i], path + "[" + i + "]"))
                        yield return pair;
                yield break;
            }

            // Objects are walked through the names the metadata actually uses; anything this misses would show up
            // as a count mismatch against System.Text.Json.
            var any = false;
            foreach (var name in Names)
            {
                var member = value[name];
                if (member == null)
                    continue;

                any = true;
                foreach (var pair in Flatten(member, path + "." + name))
                    yield return pair;
            }

            if (!any)
                yield return new KeyValuePair<string, string>(path, "v:" + value.AsBoolean + "/" + value.AsInteger);
        }

        private static IEnumerable<KeyValuePair<string, string>> Flatten(JsonElement value, string path)
        {
            switch (value.ValueKind)
            {
                case JsonValueKind.Null:
                    yield return new KeyValuePair<string, string>(path, "null");
                    break;

                case JsonValueKind.String:
                    yield return new KeyValuePair<string, string>(path, "s:" + value.GetString());
                    break;

                case JsonValueKind.Array:
                {
                    var i = 0;
                    foreach (var item in value.EnumerateArray())
                    {
                        foreach (var pair in Flatten(item, path + "[" + i + "]"))
                            yield return pair;
                        i++;
                    }
                    break;
                }

                case JsonValueKind.Object:
                {
                    foreach (var member in value.EnumerateObject())
                        foreach (var pair in Flatten(member.Value, path + "." + member.Name))
                            yield return pair;
                    break;
                }

                default:
                {
                    var boolean = value.ValueKind == JsonValueKind.True;
                    var number  = value.ValueKind == JsonValueKind.Number ? value.GetInt32() : 0;
                    yield return new KeyValuePair<string, string>(path, "v:" + boolean + "/" + number);
                    break;
                }
            }
        }

        /// <summary>Every member name JsonBuilder emits into a settings metadata file.</summary>
        private static readonly string[] Names =
        {
            "metadataVersion", "generatorVersion", "templateVersion", "include", "settings",
            "name", "type", "kind", "section", "help", "defaultValue", "inDatabaseTt", "commentedOut",
            "multiLine", "runtimeOnly", "isFlags", "enumMembers", "value", "note"
        };
    }
}
