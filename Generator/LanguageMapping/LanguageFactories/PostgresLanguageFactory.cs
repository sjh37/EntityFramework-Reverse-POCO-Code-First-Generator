namespace Efrpg.LanguageMapping.LanguageFactories
{
    public class PostgresLanguageFactory : IDatabaseLanguageFactory
    {
        public IDatabaseToPropertyType Create()
        {
            switch (Settings.GenerationLanguage)
            {
                case GenerationLanguage.CSharp:
                    return new PostgresToCSharp();

                case GenerationLanguage.Javascript:
                    // Not yet supported

                default:
                    return new PostgresToCSharp();
            }
        }
    }
}