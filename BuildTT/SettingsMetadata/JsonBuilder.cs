using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BuildTT.SettingsMetadata
{
    /// <summary>
    ///     A minimal JSON writer with fixed indentation, fixed line endings and caller-controlled property order.
    /// </summary>
    /// <remarks>
    ///     BuildTT has no JSON dependency and does not need one. The point of the metadata file is that re-running
    ///     BuildTT on an unchanged tree rewrites it byte for byte, which is what makes a dirty working copy proof
    ///     that the checked-in file went stale. A general purpose serialiser promises valid JSON, not stable bytes.
    /// </remarks>
    public class JsonBuilder
    {
        private const string NewLine = "\r\n";
        private const string Indent  = "  ";

        private readonly StringBuilder _sb         = new StringBuilder();
        private readonly Stack<bool>   _hasMembers = new Stack<bool>();

        public JsonBuilder()
        {
            _hasMembers.Push(false);
        }

        public JsonBuilder StartObject(string name)
        {
            return Open(name, '{');
        }

        public JsonBuilder EndObject()
        {
            return Close('}');
        }

        public JsonBuilder StartArray(string name)
        {
            return Open(name, '[');
        }

        public JsonBuilder EndArray()
        {
            return Close(']');
        }

        public JsonBuilder String(string name, string value)
        {
            return Member(name, value == null ? "null" : Quote(value));
        }

        public JsonBuilder Bool(string name, bool value)
        {
            return Member(name, value ? "true" : "false");
        }

        public JsonBuilder Number(string name, long value)
        {
            return Member(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public override string ToString()
        {
            return _sb + NewLine;
        }

        private JsonBuilder Open(string name, char bracket)
        {
            Separate();
            WriteName(name);
            _sb.Append(bracket);
            _hasMembers.Push(false);
            return this;
        }

        private JsonBuilder Close(char bracket)
        {
            var populated = _hasMembers.Pop();
            if (populated)
                _sb.Append(NewLine).Append(Repeat(Indent, _hasMembers.Count - 1));
            _sb.Append(bracket);
            MarkWritten();
            return this;
        }

        private JsonBuilder Member(string name, string literal)
        {
            Separate();
            WriteName(name);
            _sb.Append(literal);
            MarkWritten();
            return this;
        }

        private void Separate()
        {
            if (_hasMembers.Peek())
                _sb.Append(',');
            if (_sb.Length > 0)
                _sb.Append(NewLine).Append(Repeat(Indent, _hasMembers.Count - 1));
        }

        private void WriteName(string name)
        {
            if (name != null)
                _sb.Append(Quote(name)).Append(": ");
        }

        private void MarkWritten()
        {
            _hasMembers.Pop();
            _hasMembers.Push(true);
        }

        private static string Repeat(string text, int times)
        {
            var sb = new StringBuilder(text.Length * times);
            for (var i = 0; i < times; i++)
                sb.Append(text);
            return sb.ToString();
        }

        private static string Quote(string value)
        {
            var sb = new StringBuilder(value.Length + 2);
            sb.Append('"');

            foreach (var c in value)
            {
                switch (c)
                {
                    case '"':  sb.Append("\\\""); break;
                    case '\\': sb.Append("\\\\"); break;
                    case '\b': sb.Append("\\b");  break;
                    case '\f': sb.Append("\\f");  break;
                    case '\n': sb.Append("\\n");  break;
                    case '\r': sb.Append("\\r");  break;
                    case '\t': sb.Append("\\t");  break;
                    default:
                        if (c < ' ')
                            sb.Append("\\u").Append(((int) c).ToString("x4", CultureInfo.InvariantCulture));
                        else
                            sb.Append(c);
                        break;
                }
            }

            sb.Append('"');
            return sb.ToString();
        }
    }
}
