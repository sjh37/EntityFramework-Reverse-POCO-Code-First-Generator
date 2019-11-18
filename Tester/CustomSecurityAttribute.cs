using System;

namespace Tester
{
    public class CustomSecurityAttribute : Attribute
    {
        public CustomSecurityAttribute(SecurityEnum security)
        {
        }
    }

    public enum SecurityEnum
    {
        NoAccess,
        Readonly,
        ReadWrite,
    }
}