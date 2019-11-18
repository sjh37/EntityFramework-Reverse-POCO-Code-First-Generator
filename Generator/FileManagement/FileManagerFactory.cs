using System;

namespace Efrpg.FileManagement
{
    public static class FileManagerFactory
    {
        public static Type GetFileManagerType()
        {
            switch (Settings.FileManagerType)
            {
                case FileManagerType.VisualStudio:
                    return typeof(VisualStudioFileManager);

                case FileManagerType.Custom:
                    return typeof(CustomFileManager);

                case FileManagerType.Null:
                    return typeof(NullFileManager);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}