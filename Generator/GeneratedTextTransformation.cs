using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Efrpg
{
    // Placeholder class for compiler. Real class used in production.
    public class GeneratedTextTransformation
    {
        public StringBuilder FileData { get; private set; }

        public GeneratedTextTransformation()
        {
            FileData = new StringBuilder(2048);
        }

        public void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(CultureInfo.CurrentCulture, format, args));
        }

        public void WriteLine(string message)
        {
            LogToOutput(message);
        }

        private void LogToOutput(string message)
        {
            FileData.AppendLine(message);
        }
    }
}
