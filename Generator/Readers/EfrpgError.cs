namespace Efrpg.Readers
{
    public class EfrpgError
    {
        public string Type    { get; set; }
        public string Message { get; set; }

        public EfrpgError(string type, string message)
        {
            Type    = type;
            Message = message;
        }
    }
}
