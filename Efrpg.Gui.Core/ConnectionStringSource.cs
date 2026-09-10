using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Efrpg.Gui
{
    /// <summary>
    ///     What the <c>Settings.ConnectionString</c> line of a template says, classified into the few shapes the GUI
    ///     understands, and the value each one produces.
    /// </summary>
    /// <remarks>
    ///     A literal is the normal case and the only editable one. Two other shapes are recognised because they are
    ///     how people keep credentials out of source control: an environment variable, and a file with a literal
    ///     path. Both can be resolved here faithfully - the T4 runs inside this same Visual Studio process as this
    ///     same user, so it would see exactly the same value - which is what lets Test and the object picker work
    ///     for those templates.
    ///
    ///     Everything else is code, shown read-only with no attempt at its value. That line is deliberate and is not
    ///     to be moved: a variable argument, a configuration lookup, a helper method - each is one step from
    ///     evaluating arbitrary C# in the GUI, which is a compiler, which this project is not going to become.
    /// </remarks>
    public sealed class ConnectionStringSource
    {
        private const string Literal = @"(?:@""(?:[^""]|"""")*""|""(?:[^""\\]|\\.)*"")";

        /// <summary>
        ///     A literal; or an environment variable or file call with a literal argument, optionally trimmed,
        ///     optionally with a literal fallback after <c>??</c>. Nothing else.
        /// </summary>
        private static readonly Regex Shape = new Regex(
            @"^\s*(?:" +
            @"(?<literal>" + Literal + @")" +
            @"|(?:" +
            @"(?:System\.)?Environment\.GetEnvironmentVariable\(\s*(?<variable>" + Literal + @")\s*" +
            @"(?:,\s*(?:System\.)?EnvironmentVariableTarget\.(?<target>Process|User|Machine)\s*)?\)" +
            @"|(?:System\.IO\.)?File\.ReadAllText\(\s*(?<path>" + Literal + @")\s*\)" +
            @")" +
            @"(?<trim>\s*\.Trim\(\s*\))?" +
            @"(?:\s*\?\?\s*(?<fallback>" + Literal + @"))?" +
            @")\s*$");

        private ConnectionStringSource(ConnectionStringKind kind, string expression)
        {
            Kind       = kind;
            Expression = expression;
        }

        public ConnectionStringKind Kind { get; }

        /// <summary>The right-hand side as written in the template, trimmed. Null when <see cref="Kind"/> is Missing.</summary>
        public string Expression { get; }

        /// <summary>The value of a literal. Null for every other kind.</summary>
        public string Value { get; private set; }

        public string VariableName { get; private set; }

        /// <summary>Null when the call gave no target, which the runtime treats as the process.</summary>
        public EnvironmentVariableTarget? Target { get; private set; }

        public string Path { get; private set; }

        /// <summary>True when the line calls <c>.Trim()</c> on the result.</summary>
        public bool Trims { get; private set; }

        /// <summary>The literal after <c>??</c>, or null.</summary>
        public string Fallback { get; private set; }

        /// <summary>True when the dialog may replace the line with a literal of its own.</summary>
        public bool IsEditable => Kind == ConnectionStringKind.Literal || Kind == ConnectionStringKind.Missing;

        /// <summary>True when <see cref="Resolve"/> has a way of producing a value.</summary>
        public bool CanResolve =>
            Kind == ConnectionStringKind.Literal ||
            Kind == ConnectionStringKind.EnvironmentVariable ||
            Kind == ConnectionStringKind.File;

        /// <summary>
        ///     A short phrase for the dialog: <c>environment variable "ReversePoco" (user)</c>. Empty for a literal.
        /// </summary>
        public string Description
        {
            get
            {
                switch (Kind)
                {
                    case ConnectionStringKind.EnvironmentVariable:
                        return "environment variable \"" + VariableName + "\" (" + TargetName + ")" + FallbackNote;

                    case ConnectionStringKind.File:
                        return "the file \"" + Path + "\"" + FallbackNote;

                    case ConnectionStringKind.Expression:
                        return "code this dialog cannot evaluate";

                    default:
                        return string.Empty;
                }
            }
        }

        private string TargetName => (Target ?? EnvironmentVariableTarget.Process).ToString().ToLowerInvariant();

        private string FallbackNote => Fallback == null ? string.Empty : ", with a fallback";

        public static ConnectionStringSource ForLiteral(string value)
        {
            return new ConnectionStringSource(ConnectionStringKind.Literal, SettingValue.WriteText(value, false)) { Value = value ?? string.Empty };
        }

        /// <summary>Reads the live <c>Settings.ConnectionString</c> line of a template.</summary>
        public static ConnectionStringSource Read(TemplateSettingsFile settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            // A literal first, through the reader that understands a semicolon inside the quotes - which every
            // connection string has, and which the general expression reader stops at.
            var literal = settings.GetString("ConnectionString");
            if (literal != null)
                return ForLiteral(literal);

            return Parse(settings.GetExpression("ConnectionString"));
        }

        /// <summary>Classifies one right-hand side. Null means the template has no such line.</summary>
        public static ConnectionStringSource Parse(string expression)
        {
            if (expression == null)
                return new ConnectionStringSource(ConnectionStringKind.Missing, null);

            var trimmed = expression.Trim();
            var match   = Shape.Match(trimmed);

            if (!match.Success)
                return new ConnectionStringSource(ConnectionStringKind.Expression, trimmed);

            if (match.Groups["literal"].Success)
                return new ConnectionStringSource(ConnectionStringKind.Literal, trimmed) { Value = Unquote(match.Groups["literal"].Value) };

            var source = match.Groups["variable"].Success
                ? new ConnectionStringSource(ConnectionStringKind.EnvironmentVariable, trimmed) { VariableName = Unquote(match.Groups["variable"].Value) }
                : new ConnectionStringSource(ConnectionStringKind.File, trimmed) { Path = Unquote(match.Groups["path"].Value) };

            if (match.Groups["target"].Success)
                source.Target = (EnvironmentVariableTarget) Enum.Parse(typeof(EnvironmentVariableTarget), match.Groups["target"].Value);

            source.Trims    = match.Groups["trim"].Success;
            source.Fallback = match.Groups["fallback"].Success ? Unquote(match.Groups["fallback"].Value) : null;

            return source;
        }

        /// <summary>
        ///     The connection string this line produces right now, or null with a reason. Never throws: a missing
        ///     variable or an unreadable file is the user's situation to be told about, not a fault.
        /// </summary>
        public string Resolve(out string error)
        {
            error = null;

            switch (Kind)
            {
                case ConnectionStringKind.Literal:
                    return Value;

                case ConnectionStringKind.EnvironmentVariable:
                    return Finish(Target.HasValue
                            ? Environment.GetEnvironmentVariable(VariableName, Target.Value)
                            : Environment.GetEnvironmentVariable(VariableName),
                        "Environment variable \"" + VariableName + "\" (" + TargetName + ") is not set on this machine, so there is no connection string to use.",
                        out error);

                case ConnectionStringKind.File:
                    return ResolveFile(out error);

                case ConnectionStringKind.Missing:
                    error = "The template has no Settings.ConnectionString line.";
                    return null;

                default:
                    error = "The connection string is set in code this dialog cannot evaluate: " + Expression +
                            ". Edit it in the .tt to change it.";
                    return null;
            }
        }

        private string ResolveFile(out string error)
        {
            string content;

            try
            {
                content = File.ReadAllText(Path);
            }
            catch (Exception ex)
            {
                error = "The connection string file \"" + Path + "\" could not be read: " + ex.Message;
                return null;
            }

            return Finish(content, "The connection string file \"" + Path + "\" is empty.", out error);
        }

        /// <summary>Applies <c>.Trim()</c> and the <c>??</c> fallback the way the template's own line would.</summary>
        private string Finish(string value, string emptyMessage, out string error)
        {
            error = null;

            if (value != null && Trims)
                value = value.Trim();

            if (!string.IsNullOrEmpty(value))
                return value;

            if (Fallback != null)
                return Fallback;

            error = emptyMessage;
            return null;
        }

        private static string Unquote(string literal)
        {
            string value;
            bool isVerbatim;

            return SettingValue.TryReadText(literal, out value, out isVerbatim) ? value : literal;
        }
    }
}
