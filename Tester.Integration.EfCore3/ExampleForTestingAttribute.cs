using System;

namespace Tester.Integration.EfCore3
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ExampleForTestingAttribute : Attribute
    {
        private readonly string _parameter;

        public ExampleForTestingAttribute(string parameter)
        {
            _parameter = parameter;
        }
    }
}