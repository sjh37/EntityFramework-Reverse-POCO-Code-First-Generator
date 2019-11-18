using System;
using System.Collections.Generic;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Represents a scope of keys.
    /// </summary>
    public sealed class Scope
    {
        private readonly object _source;
        private readonly Scope _parent;

        /// <summary>
        /// Initializes a new instance of a KeyScope.
        /// </summary>
        /// <param name="source">The object to search for keys in.</param>
        internal Scope(object source)
            : this(source, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of a KeyScope.
        /// </summary>
        /// <param name="source">The object to search for keys in.</param>
        /// <param name="parent">The parent scope to search in if the value is not found.</param>
        internal Scope(object source, Scope parent)
        {
            _parent = parent;
            _source = source;
        }

        /// <summary>
        /// Occurs when a key/property is found in the object graph.
        /// </summary>
        public event EventHandler<KeyFoundEventArgs> KeyFound;

        /// <summary>
        /// Occurs when a key/property is not found in the object graph.
        /// </summary>
        public event EventHandler<KeyNotFoundEventArgs> KeyNotFound;

        /// <summary>
        /// Occurs when a setter is encountered and requires a value to be provided.
        /// </summary>
        public event EventHandler<ValueRequestEventArgs> ValueRequested;

        /// <summary>
        /// Creates a child scope that searches for keys in a default dictionary of key/value pairs.
        /// </summary>
        /// <returns>The new child scope.</returns>
        public Scope CreateChildScope()
        {
            return CreateChildScope(new Dictionary<string, object>());
        }

        /// <summary>
        /// Creates a child scope that searches for keys in the given object.
        /// </summary>
        /// <param name="source">The object to search for keys in.</param>
        /// <returns>The new child scope.</returns>
        public Scope CreateChildScope(object source)
        {
            Scope scope = new Scope(source, this);
            scope.KeyFound = KeyFound;
            scope.KeyNotFound = KeyNotFound;
            scope.ValueRequested = ValueRequested;
            return scope;
        }

        /// <summary>
        /// Attempts to find the value associated with the key with given name.
        /// </summary>
        /// <param name="name">The name of the key.</param>
        /// <param name="isExtension">Specifies whether the key appeared within triple curly braces.</param>
        /// <returns>The value associated with the key with the given name.</returns>
        internal object Find(string name, bool isExtension)
        {
            SearchResults results = tryFind(name);
            if (results.Found)
            {
                return onKeyFound(name, results.Value, isExtension);
            }

            object value;
            if (onKeyNotFound(name, results.Member, isExtension, out value))
            {
                return value;
            }

            // if the key was not found, return the original tag
            return "{{" + name + "}}";
        }

        private object onKeyFound(string name, object value, bool isExtension)
        {
            if (KeyFound == null)
            {
                return value;
            }

            KeyFoundEventArgs args = new KeyFoundEventArgs(name, value, isExtension);
            KeyFound(this, args);
            return args.Substitute;
        }

        private bool onKeyNotFound(string name, string member, bool isExtension, out object value)
        {
            if (KeyNotFound == null)
            {
                value = null;
                return false;
            }
            KeyNotFoundEventArgs args = new KeyNotFoundEventArgs(name, member, isExtension);
            KeyNotFound(this, args);
            if (!args.Handled)
            {
                value = null;
                return false;
            }
            value = args.Substitute;
            return true;
        }

        private static IDictionary<string, object> toLookup(object value)
        {
            IDictionary<string, object> lookup = UpcastDictionary.Create(value);
            if (lookup == null)
            {
                lookup = new PropertyDictionary(value);
            }
            return lookup;
        }

        internal void Set(string key)
        {
            SearchResults results = tryFind(key);
            if (ValueRequested == null)
            {
                set(results, results.Value);
                return;
            }

            ValueRequestEventArgs e = new ValueRequestEventArgs();
            if (results.Found)
            {
                e.Value = results.Value;
            }

            ValueRequested(this, e);
            set(results, e.Value);
        }

        internal void Set(string key, object value)
        {
            SearchResults results = tryFind(key);
            set(results, value);
        }

        private void set(SearchResults results, object value)
        {
            // handle setting value in child scope
            while (results.MemberIndex < results.Members.Length - 1)
            {
                Dictionary<string, object> context = new Dictionary<string, object>();
                results.Value = context;
                results.Lookup[results.Member] = results.Value;
                results.Lookup = context;
                ++results.MemberIndex;
            }
            results.Lookup[results.Member] = value;
        }

        public bool TryFind(string name, out object value)
        {
            SearchResults result = tryFind(name);
            value = result.Value;
            return result.Found;
        }

        private SearchResults tryFind(string name)
        {
            SearchResults results = new SearchResults();
            results.Members = name.Split('.');
            results.MemberIndex = 0;
            if (results.Member == "this")
            {
                results.Found = true;
                results.Lookup = toLookup(_source);
                results.Value = _source;
            }
            else
            {
                tryFindFirst(results);
            }
            for (int index = 1; results.Found && index < results.Members.Length; ++index)
            {
                results.Lookup = toLookup(results.Value);
                results.MemberIndex = index;
                object value;
                results.Found = results.Lookup.TryGetValue(results.Member, out value);
                results.Value = value;
            }
            return results;
        }

        private void tryFindFirst(SearchResults results)
        {
            results.Lookup = toLookup(_source);
            object value;
            if (results.Lookup.TryGetValue(results.Member, out value))
            {
                results.Found = true;
                results.Value = value;
                return;
            }
            if (_parent == null)
            {
                results.Found = false;
                results.Value = null;
                return;
            }
            _parent.tryFindFirst(results);
        }
    }

    internal class SearchResults
    {
        public IDictionary<string, object> Lookup { get; set; }

        public string[] Members { get; set; }

        public int MemberIndex { get; set; }

        public string Member { get { return Members[MemberIndex]; } }

        public bool Found { get; set; }

        public object Value { get; set; }
    }
}
