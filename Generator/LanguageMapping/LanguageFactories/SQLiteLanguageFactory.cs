using System;

namespace Efrpg.LanguageMapping.LanguageFactories
{
    public class SQLiteLanguageFactory : IDatabaseLanguageFactory
    {
        public IDatabaseToPropertyType Create()
        {
            switch (Settings.GenerationLanguage)
            {
                case GenerationLanguage.CSharp:
                    return new SqLiteToCSharp();

                case GenerationLanguage.Javascript:
                    // Not yet supported

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}