using System;

namespace Efrpg.FileManagement
{
    public static class FileManagerFactory
    {
        public static Type GetFileManagerType()
        {

            return typeof(EfCoreFileManager);
        }
    }
}