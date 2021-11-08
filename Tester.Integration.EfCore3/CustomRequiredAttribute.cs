using System;

namespace Tester.Integration.EfCore3
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class CustomRequiredAttribute : Attribute
    {
    }
}