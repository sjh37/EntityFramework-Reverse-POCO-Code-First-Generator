using System;

namespace Efrpg.LanguageMapping.LanguageFactories
{
    public class SqlServerLanguageFactory : IDatabaseLanguageFactory
    {
        public IDatabaseToPropertyType Create()
        {
            switch (Settings.GenerationLanguage)
            {
                case GenerationLanguage.CSharp:
                    return new SqlServerToCSharp();

                case GenerationLanguage.Javascript:
                    return new SqlServerToJavascript();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}