using System.Xml.Linq;

namespace Efrpg.Readers
{
    // Template side of the secrets exchange. Connection strings are handed to the efrpg tool over stdin instead of on
    // the command line, so they never appear in process listings or in the command-line audit trail collected by
    // Sysmon, EDR agents and ETW tracing.
    //
    // Encryption was considered and rejected: both processes run as the same user with no shared secret, so any key
    // would have to ship inside this plaintext .ttinclude and would be readable by anyone able to read the command
    // line in the first place. Keeping the value off the command line is the actual fix.
    public static class SecretsXml
    {
        public static string Write(string connection, string multiContextConnection)
        {
            var root = new XElement("Secrets",
                new XElement("Connection", connection ?? string.Empty));

            if (!string.IsNullOrWhiteSpace(multiContextConnection))
                root.Add(new XElement("MultiContextConnection", multiContextConnection));

            return root.ToString(SaveOptions.DisableFormatting);
        }
    }
}
