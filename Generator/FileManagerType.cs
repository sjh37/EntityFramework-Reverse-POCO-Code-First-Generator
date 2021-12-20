using System;

namespace Efrpg
{
    public enum FileManagerType
    {
        // Use this for .NET 4.x projects.
        // It will make use of the `EF6.Utility.CS.ttinclude` to add/remove files to the Visual Studio project.
        VisualStudio,

        // Use this for .NET Core projects.
        // It will write the files directly.
        // Visual Studio will automatically detect new files and include them in the project.
        EfCore,
        
        [Obsolete("Please use FileManagerType.EfCore instead")]
        Custom,
        
        Null // For testing only. Does nothing.
    }
}