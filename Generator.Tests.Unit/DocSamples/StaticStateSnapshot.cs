using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Efrpg;
using Efrpg.Filtering;

namespace Generator.Tests.Unit.DocSamples
{
    /// <summary>
    ///     Captures the static state the generator hangs off, so a fixture that has to change it can put it back.
    /// </summary>
    /// <remarks>
    ///     <c>Settings</c>, <c>FilterSettings</c> and <c>Inflector</c> are static and shared by every test in the
    ///     process. Producing a doc sample means changing a lot of them, which quietly changed the answers other
    ///     fixtures got - <c>ForeignKeyTests</c> passed alone and failed in a full run, which is the worst kind of
    ///     failure to chase.
    ///     Restoring by hand is not an option: several settings are delegates with real logic in them, and writing
    ///     them out again here would be a second copy of Settings.cs to keep in step. Reflecting over the static
    ///     fields captures the delegates as they are, whatever they are.
    ///     Fields only. The two settings exposed as properties, DbContextInterfaceName and
    ///     DefaultConstructorArgument, store their values in private static fields, so they are captured too.
    /// </remarks>
    public sealed class StaticStateSnapshot
    {
        private readonly Dictionary<FieldInfo, object> _fields = new Dictionary<FieldInfo, object>();
        private readonly Dictionary<FieldInfo, IList> _listContents = new Dictionary<FieldInfo, IList>();

        private static readonly Type[] Owners = { typeof(Settings), typeof(FilterSettings), typeof(Inflector) };

        public static StaticStateSnapshot Capture()
        {
            var snapshot = new StaticStateSnapshot();

            foreach (var owner in Owners)
            {
                foreach (var field in owner.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (field.IsLiteral)
                        continue; // A const has no storage to restore

                    var value = field.GetValue(null);

                    if (field.IsInitOnly)
                    {
                        // A readonly field cannot be reassigned, so for the filter lists capture the contents
                        // instead and restore them element by element.
                        var list = value as IList;
                        if (list != null)
                            snapshot._listContents[field] = Copy(list);
                        continue;
                    }

                    snapshot._fields[field] = value;
                }
            }

            return snapshot;
        }

        public void Restore()
        {
            foreach (var entry in _fields)
                entry.Key.SetValue(null, entry.Value);

            foreach (var entry in _listContents)
            {
                var live = (IList)entry.Key.GetValue(null);
                live.Clear();
                foreach (var item in entry.Value)
                    live.Add(item);
            }
        }

        private static IList Copy(IList source)
        {
            var copy = new List<object>(source.Count);
            foreach (var item in source)
                copy.Add(item);
            return copy;
        }
    }
}
