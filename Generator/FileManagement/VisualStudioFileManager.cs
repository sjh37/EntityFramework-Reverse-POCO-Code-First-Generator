namespace Efrpg.FileManagement
{
    // This will make use of the real file manager within EF6.Utility.CS.ttinclude
    public class VisualStudioFileManager : IFileManager
    {
        private EntityFrameworkTemplateFileManager _wrapped;

        public void Init(GeneratedTextTransformation textTransformation)
        {
            _wrapped = EntityFrameworkTemplateFileManager.Create(textTransformation);
        }

        public void StartHeader()
        {
            _wrapped.StartHeader();
        }

        public void StartFooter()
        {
            _wrapped.StartFooter();
        }

        public void EndBlock()
        {
            _wrapped.EndBlock();
        }

        public void Process(bool split)
        {
            _wrapped.Process(split);
        }

        public void StartNewFile(string name)
        {
            _wrapped.StartNewFile(name);
        }

        public void WriteLine(string text)
        {
        }
    }
}