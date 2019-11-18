namespace Efrpg.Mustache
{
    public class PlaceholderArgument : IArgument
    {
        private readonly string name;

        public PlaceholderArgument(string name)
        {
            this.name = name;
        }

        public string GetKey()
        {
            return name;
        }

        public object GetValue(Scope keyScope, Scope contextScope)
        {
            return keyScope.Find(name, false);
        }
    }
}
