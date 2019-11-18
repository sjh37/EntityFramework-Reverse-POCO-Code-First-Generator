namespace Efrpg.LanguageMapping.LanguageFactories
{
    public class PluginLanguageFactory : IDatabaseLanguageFactory
    {
        public IDatabaseToPropertyType Create()
        {
            return null; // Will ask plugin for it
        }
    }
}