namespace Efrpg.Mustache
{
    public class VariableArgument : IArgument
    {
        private readonly string name;

        public VariableArgument(string name)
        {
            this.name = name;
        }

        public string GetKey()
        {
            return null;
        }

        public object GetValue(Scope keyScope, Scope contextScope)
        {
            return contextScope.Find(name, false);
        }
    }
}
