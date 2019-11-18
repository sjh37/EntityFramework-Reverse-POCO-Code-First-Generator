// DO NOT USE


#load "./utils.cake"
#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories
var sln = MakeAbsolute(File(Argument("solutionPath", "./EF.Reverse.POCO.GeneratorV3.sln")));

// Root
var generatorRoot = Directory("./Generator");
var ttRoot = Directory("./EntityFramework.Reverse.POCO.Generator");

// bin/release
var generatorDir = Directory("./Generator/bin") + Directory(configuration);
var ttDir = Directory("./EntityFramework.Reverse.POCO.Generator/bin") + Directory(configuration);
var vsixDir = Directory("./EntityFramework Reverse POCO Generator/bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(generatorDir);
    CleanDirectory(ttDir);
    CleanDirectory(vsixDir);
});

Task("CreateTT")
    .IsDependentOn("Clean")
    .Does(() =>
{
    // todo create tt file from generator files
});

Task("Pack")
    .IsDependentOn("CreateTT")
    .Does(() =>
{
    // todo see pack.bat
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Pack")
    .Does(() =>
{
    NuGetRestore(sln);
});


Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
        // Use MSBuild
        MSBuild(sln, settings => settings
            .SetConfiguration(configuration)
            .UseToolVersion(MSBuildToolVersion.VS2017)
            .SetMSBuildPlatform(MSBuildPlatform.x86)
        );
    }
    else
    {
        // Use XBuild
        XBuild(sln, settings => settings.SetConfiguration(configuration));
    }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
