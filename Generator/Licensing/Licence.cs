using System;
using System.Globalization;
using System.Linq;

namespace Efrpg.Licensing
{
    public class license
    {
        public string      RegisteredTo { get; private set; }
        public string      Company      { get; private set; }
        public licenseType licenseType  { get; private set; }
        public string      Numlicenses  { get; private set; }
        public DateTime    ValidUntil   { get; private set; }

        public license(string registeredTo, string company, licenseType licenseType, string numlicenses, DateTime validUntil)
        {
            RegisteredTo = registeredTo;
            Company      = company;
            licenseType  = licenseType;
            Numlicenses  = numlicenses;
            ValidUntil   = validUntil;
        }

        public string GetlicenseType()
        {
            return GetlicenseType(licenseType);
        }

        public static string GetlicenseType(licenseType licenseType)
        {
            switch (licenseType)
            {
                case licenseType.Academic:
                    return "Academic license - for non-commercial use only";

                case licenseType.Commercial:
                    return "Commercial";

                case licenseType.Trial:
                    return "Trial - for non-commercial trial use only";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static licenseType ParselicenseType(string licenseType)
        {
            licenseType = licenseType.Substring(0, 5);
            foreach (var type in Enum.GetValues(typeof(licenseType)).Cast<licenseType>())
            {
                if (GetlicenseType(type).Substring(0, 5) == licenseType)
                    return type;
            }
            throw new ArgumentOutOfRangeException();
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}",
                RegisteredTo.ToUpperInvariant().Trim(),
                Company     .ToUpperInvariant().Trim(),
                GetlicenseType(),
                Numlicenses .ToUpperInvariant().Trim(),
                ValidUntil  .ToString(licenseConstants.ExpiryFormat, CultureInfo.InvariantCulture).ToUpperInvariant());
        }
    }
}