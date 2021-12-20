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

#pragma warning disable CS0618 // Type or member is obsolete
                case FileManagerType.Custom:
#pragma warning restore CS0618 // Type or member is obsolete
                case FileManagerType.EfCore:
                    return typeof(EfCoreFileManager);

                case FileManagerType.Null:
                    return typeof(NullFileManager);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}