namespace Efrpg.FileManagement
{
    public interface IFileManager
    {
        void Init(GeneratedTextTransformation textTransformation);
        void StartHeader();
        void StartFooter();
        void EndBlock();
        void Process(bool split);
        void StartNewFile(string name);
        void WriteLine(string text);
    }
}