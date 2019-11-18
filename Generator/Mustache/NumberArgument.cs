namespace Efrpg.Mustache
{
    public class NumberArgument : IArgument
    {
        private readonly decimal value;

        public NumberArgument(decimal value)
        {
            this.value = value;
        }

        public string GetKey()
        {
            return null;
        }

        public object GetValue(Scope keyScope, Scope contextScope)
        {
            return value;
        }
    }
}
