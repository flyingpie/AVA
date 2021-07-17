#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0"
#tool "nuget:?package=vswhere&version=2.6.7"

using static System.IO.Path;

var configuration = Argument("configuration", "Release");
var output = Argument("output", "artifacts");
var platform = Argument("platform", PlatformTarget.x64);

var sln = "src/Ava.sln";

Task("Clean").Does(() =>
{
	CleanDirectory(output);
});

Task("Build.MSIL").Does(() =>
{
	MSBuild(sln, new MSBuildSettings
	{
		Configuration = configuration,
		PlatformTarget = platform,
		Restore = true,
		ToolPath = GetFiles(VSWhereLatest() + "/**/MSBuild.exe").FirstOrDefault(),
	});
});

Task("Build").Does(() =>
{
	MSBuild(sln, new MSBuildSettings
	{
		Configuration = configuration,
		PlatformTarget = platform,
		Restore = true,
		ToolPath = GetFiles(VSWhereLatest() + "/**/MSBuild.exe").FirstOrDefault(),
	}
		.WithProperty("runtime", "win10-x64")
	);
});

Task("Artifact.App").Does(() =>
{
	var bin = Combine(output, $"AVA");
	CreateDirectory(bin);

	var skipDirs = new [] { "AVA.Core", "MUI", "MUI.UWP" };

	foreach (var dir in GetSubDirectories("build/bin/"))
	{
		var dirName = dir.GetDirectoryName();

		if (skipDirs.Contains(dirName)) continue;

		Information("SUP: " + dir);

		CopyFiles(Combine(dir.FullPath, "x64/Release/net472/**/*"), bin, true);
	}

	DeleteFiles($"{bin}/**/*.pdb");
	DeleteFiles($"{bin}/**/*.winmd");
	DeleteFiles($"{bin}/**/*.xml");
});

Task("Default")
	.IsDependentOn("Clean")
	.IsDependentOn("Build")
	.IsDependentOn("Artifact.App")
	.Does(() => {})
;

RunTarget("Default");
