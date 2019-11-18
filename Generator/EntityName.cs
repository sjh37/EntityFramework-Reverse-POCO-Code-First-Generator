namespace Efrpg
{
    public abstract class EntityName
    {
        public string DbName;        // Raw name as obtained from the database
        public string NameHumanCase; // Name adjusted for C# generator output
    }
}