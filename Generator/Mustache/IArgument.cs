namespace Efrpg.Mustache
{
    public interface IArgument
    {
        string GetKey();

        object GetValue(Scope keyScope, Scope contextScope);
    }
}
