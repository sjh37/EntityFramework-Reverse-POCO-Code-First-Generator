namespace Efrpg.Generators
{
    using System;

    public static class ExceptionExtension
    {
        public static string FormatError(this Exception ex)
        {
            return ex.Message.Replace("\r\n", "\n").Replace("\n", " ");
        }
    }
}