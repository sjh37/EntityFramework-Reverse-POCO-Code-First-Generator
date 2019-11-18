namespace Efrpg.LanguageMapping.LanguageFactories
{
    public class OracleLanguageFactory : IDatabaseLanguageFactory
    {
        public IDatabaseToPropertyType Create()
        {
            switch (Settings.GenerationLanguage)
            {
                case GenerationLanguage.CSharp:
                    return new MySqlToCSharp();

                case GenerationLanguage.Javascript:
                    // Not yet supported

                default:
                    return new MySqlToCSharp();
            }
        }
    }
}