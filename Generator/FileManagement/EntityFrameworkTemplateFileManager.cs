using System.Collections.Generic;

namespace Efrpg.FileManagement
{
    // Placeholder class for compiler. Real class used in production.
    internal class EntityFrameworkTemplateFileManager
    {
        public static EntityFrameworkTemplateFileManager Create(object textTransformation)
        {
            return new EntityFrameworkTemplateFileManager();
        }

        public void StartNewFile(string name)
        {
        }

        public void StartFooter()
        {
        }

        public void StartHeader()
        {
        }

        public void EndBlock()
        {
        }

        public virtual IEnumerable<string> Process(bool split)
        {
            return new List<string>();
        }
    }
}