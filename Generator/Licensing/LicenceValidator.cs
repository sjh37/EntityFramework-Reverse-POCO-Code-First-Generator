using System;
using System.Globalization;
using System.Text;

namespace Efrpg.Licensing
{
    public class licenseValidator
    {
        private readonly DigitalSignaturePublic _ds;
        public license license;
        public bool Expired;

        public licenseValidator()
        {
            _ds = new DigitalSignaturePublic();
        }

        public bool Validate(string licenseInput)
        {
            try
            {
                var array = licenseInput.Replace("\n", "\r").Replace("\r\r", "\r").Trim().Split('\r');

                var expiryText = ParseString(array, licenseConstants.ValidUntil);
                var parsedExpiry = DateTime.ParseExact(expiryText, licenseConstants.ExpiryFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                var expiryEndOfDay = new DateTime(parsedExpiry.Year, parsedExpiry.Month, parsedExpiry.Day, 23, 59, 59, DateTimeKind.Local);
                Expired = expiryEndOfDay < DateTime.Now;
                if (Expired)
                    return false;

                license = new license(
                    ParseString(array, licenseConstants.RegisteredTo),
                    ParseString(array, licenseConstants.Company),
                    license.ParselicenseType(ParseString(array, licenseConstants.licenseType)),
                    ParseString(array, licenseConstants.Numlicenses),
                    expiryEndOfDay);

                var foundSignature = false;
                var sigUpperCase = licenseConstants.Signature;
                var signature = new StringBuilder(1024);
                foreach (var line in array)
                {
                    if (foundSignature)
                        signature.Append(line);
                    else
                    if (line.StartsWith(sigUpperCase))
                        foundSignature = true;
                }

                return _ds.VerifySignature(license.ToString(), Hex.HexToByteArray(signature.ToString().Trim()));
            }
            catch
            {
                return false;
            }
        }

        private string ParseString(string[] array, string find)
        {
            foreach (var line in array)
            {
                if (line.StartsWith(find))
                    return line.Substring(line.IndexOf(':') + 2).Trim();
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}