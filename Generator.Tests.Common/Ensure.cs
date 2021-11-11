using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Generator.Tests.Common
{
    [DebuggerStepThrough]
    [ExcludeFromCodeCoverage]
    public static class Ensure
    {
        public static void EnsureIsNotNull(this object subject, string name = null, string message = "")
        {
            IsNotNull(subject, name, message);
        }

        public static void IsNotNull(object subject, string name = null, string message = "")
        {
            if (subject == null)
            {
                throw new ArgumentNullException(name ?? nameof(subject), message ?? "Object cannot be null");
            }
        }

        public static void IsNotNullOrEmpty(string subject, string name = null, string message = "")
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentNullException(name ?? nameof(subject), message ?? "Object cannot be null or empty");
            }
        }
    }
}