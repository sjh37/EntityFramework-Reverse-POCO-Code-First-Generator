using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Efrpg.Gui
{
    /// <summary>
    ///     A parsed JSON value: object, array, string, number, boolean or null.
    /// </summary>
    /// <remarks>
    ///     Hand written rather than taken from a package, for the same reason BuildTT's JsonBuilder is: this
    ///     assembly is loaded in process by Visual Studio, which brings its own copy of most things, and a
    ///     serialiser dependency there is a binding conflict waiting to happen for no benefit. The only JSON it
    ///     ever reads is settings-metadata.v3.json and v4.json, both written by JsonBuilder in this same repository.
    ///
    ///     Being narrow is the safety argument, so it is checked rather than assumed: the tests parse both shipped
    ///     metadata files with this and with System.Text.Json and assert the two agree, member for member.
    /// </remarks>
    public sealed class Json
    {
        private readonly object _value;

        private Json(object value)
        {
            _value = value;
        }

        /// <summary>Parses a complete JSON document. Throws <see cref="FormatException"/> on anything malformed.</summary>
        public static Json Parse(string text)
        {
            var position = 0;
            var value = ReadValue(text ?? string.Empty, ref position);

            SkipWhitespace(text, ref position);
            if (position != text.Length)
                throw new FormatException("Unexpected text after the JSON document, at " + position + ".");

            return value;
        }

        /// <summary>The named member of an object, or null when absent. Null for anything that is not an object.</summary>
        public Json this[string name]
        {
            get
            {
                var members = _value as Dictionary<string, Json>;
                Json member;

                return members != null && members.TryGetValue(name, out member) ? member : null;
            }
        }

        /// <summary>The elements of an array, or an empty list for anything else.</summary>
        public IReadOnlyList<Json> Items => _value as List<Json> ?? (IReadOnlyList<Json>) new Json[0];

        /// <summary>The value as text, or null when it is not a string.</summary>
        public string AsString => _value as string;

        public bool AsBoolean => _value is bool && (bool) _value;

        public int AsInteger => _value is double ? (int) (double) _value : 0;

        public bool IsNull => _value == null;

        private static Json ReadValue(string text, ref int position)
        {
            SkipWhitespace(text, ref position);

            if (position >= text.Length)
                throw new FormatException("The JSON document ended unexpectedly.");

            switch (text[position])
            {
                case '{': return ReadObject(text, ref position);
                case '[': return ReadArray(text, ref position);
                case '"': return new Json(ReadString(text, ref position));
                case 't': return ReadKeyword(text, ref position, "true", true);
                case 'f': return ReadKeyword(text, ref position, "false", false);
                case 'n': return ReadKeyword(text, ref position, "null", null);
                default:  return new Json(ReadNumber(text, ref position));
            }
        }

        private static Json ReadObject(string text, ref int position)
        {
            var members = new Dictionary<string, Json>(StringComparer.Ordinal);

            position++; // {
            SkipWhitespace(text, ref position);

            if (Peek(text, position) == '}')
            {
                position++;
                return new Json(members);
            }

            while (true)
            {
                SkipWhitespace(text, ref position);

                var name = ReadString(text, ref position);
                SkipWhitespace(text, ref position);
                Expect(text, ref position, ':');

                members[name] = ReadValue(text, ref position);
                SkipWhitespace(text, ref position);

                if (Peek(text, position) == ',')
                {
                    position++;
                    continue;
                }

                Expect(text, ref position, '}');
                return new Json(members);
            }
        }

        private static Json ReadArray(string text, ref int position)
        {
            var items = new List<Json>();

            position++; // [
            SkipWhitespace(text, ref position);

            if (Peek(text, position) == ']')
            {
                position++;
                return new Json(items);
            }

            while (true)
            {
                items.Add(ReadValue(text, ref position));
                SkipWhitespace(text, ref position);

                if (Peek(text, position) == ',')
                {
                    position++;
                    continue;
                }

                Expect(text, ref position, ']');
                return new Json(items);
            }
        }

        private static string ReadString(string text, ref int position)
        {
            Expect(text, ref position, '"');

            var result = new StringBuilder();

            while (position < text.Length)
            {
                var c = text[position++];

                if (c == '"')
                    return result.ToString();

                if (c != '\\')
                {
                    result.Append(c);
                    continue;
                }

                if (position >= text.Length)
                    break;

                var escape = text[position++];
                switch (escape)
                {
                    case '"':  result.Append('"');  break;
                    case '\\': result.Append('\\'); break;
                    case '/':  result.Append('/');  break;
                    case 'b':  result.Append('\b'); break;
                    case 'f':  result.Append('\f'); break;
                    case 'n':  result.Append('\n'); break;
                    case 'r':  result.Append('\r'); break;
                    case 't':  result.Append('\t'); break;
                    case 'u':
                        if (position + 4 > text.Length)
                            throw new FormatException("Truncated \\u escape at " + position + ".");
                        result.Append((char) int.Parse(text.Substring(position, 4), NumberStyles.HexNumber,
                            CultureInfo.InvariantCulture));
                        position += 4;
                        break;
                    default:
                        throw new FormatException("Unknown escape '\\" + escape + "' at " + position + ".");
                }
            }

            throw new FormatException("Unterminated string.");
        }

        private static double ReadNumber(string text, ref int position)
        {
            var start = position;

            while (position < text.Length && "+-.eE0123456789".IndexOf(text[position]) >= 0)
                position++;

            double parsed;
            if (position == start || !double.TryParse(text.Substring(start, position - start),
                    NumberStyles.Float, CultureInfo.InvariantCulture, out parsed))
                throw new FormatException("Expected a value at " + start + ".");

            return parsed;
        }

        private static Json ReadKeyword(string text, ref int position, string keyword, object value)
        {
            if (position + keyword.Length > text.Length ||
                string.CompareOrdinal(text, position, keyword, 0, keyword.Length) != 0)
                throw new FormatException("Expected '" + keyword + "' at " + position + ".");

            position += keyword.Length;
            return new Json(value);
        }

        private static void SkipWhitespace(string text, ref int position)
        {
            while (position < text.Length && char.IsWhiteSpace(text[position]))
                position++;
        }

        private static char Peek(string text, int position)
        {
            return position < text.Length ? text[position] : '\0';
        }

        private static void Expect(string text, ref int position, char expected)
        {
            if (Peek(text, position) != expected)
                throw new FormatException("Expected '" + expected + "' at " + position + ".");

            position++;
        }
    }
}
