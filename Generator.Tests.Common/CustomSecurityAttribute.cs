namespace Generator.Tests.Common
{
    using System;

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