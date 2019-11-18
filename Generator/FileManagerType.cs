namespace Efrpg
{
    public enum FileManagerType
    {
        VisualStudio, // Use this for .NET 4.x projects. It will make use of the `EF6.Utility.CS.ttinclude` to add/remove files from the Visual Studio project.
        Custom, // Use this for .NET Core projects. It will write the files directly. Visual Studio will automatically detect new files and include them in the project.
        Null // For testing only. Does nothing.
    }
}