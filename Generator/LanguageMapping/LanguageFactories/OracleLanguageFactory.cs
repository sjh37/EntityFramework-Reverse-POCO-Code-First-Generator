namespace Efrpg.LanguageMapping.LanguageFactories
{
    public class OracleLanguageFactory : IDatabaseLanguageFactory
    {
        public IDatabaseToPropertyType Create()
        {
            switch (Settings.GenerationLanguage)
            {
                case GenerationLanguage.CSharp:
                    return new OracleToCSharp();

                case GenerationLanguage.Javascript:
                    // Not yet supported

                default:
                    return new OracleToCSharp();
            }
        }
    }
}