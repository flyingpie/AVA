#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0"
#tool "nuget:?package=vswhere&version=2.5.2"

var configuration = Argument("configuration", "Release");
var output = Argument("output", "bin");

var sln = "src/Ava.sln";

var vsLatest  = VSWhereLatest();
var msBuild = vsLatest.CombineWithFilePath("./MSBuild/15.0/Bin/MSBuild.exe");
var vsTest = vsLatest.CombineWithFilePath("./Common7/IDE/CommonExtensions/Microsoft/TestWindow/vstest.console.exe");

Task("Default").Does(() =>
{
    // CLEAN //
    CleanDirectory(output);

    // RESTORE //
    NuGetRestore(sln);

    // BUILD //
    MSBuild(sln, new MSBuildSettings
    {
        Configuration = configuration,
        MaxCpuCount = 0, // As many as available
        NodeReuse = false, // Required to prevent build task dll's from being locked
        ToolPath = msBuild
    });

    // TEST //
    VSTest("./bin/**/*.UnitTest.dll", new VSTestSettings
    {
        ToolPath = vsTest
    });

    // CLEANUP //
    DeleteFiles($"{output}/**/*.pdb");
    DeleteFiles($"{output}/**/*.winmd");
    DeleteFiles($"{output}/**/*.xml");

    DeleteFiles($"{output}/AVA/settings.json");

    System.IO.Directory.Delete($"{output}/AVA.Core", true);
    System.IO.Directory.Delete($"{output}/AVA.Indexing", true);

    System.IO.Directory.Delete($"{output}/MUI", true);
    System.IO.Directory.Delete($"{output}/MUI.Scripting", true);
    System.IO.Directory.Delete($"{output}/MUI.Win32", true);

    System.IO.Directory.Delete($"{output}/obj", true);
});

RunTarget("Default");
