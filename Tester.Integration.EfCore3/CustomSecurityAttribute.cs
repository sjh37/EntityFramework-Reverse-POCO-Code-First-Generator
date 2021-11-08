using System;

namespace Tester.Integration.EfCore3
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
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