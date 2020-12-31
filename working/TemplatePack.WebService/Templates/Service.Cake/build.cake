/////////////////////////////////////////////////////////////////
//						ARGUMENTS							   //
////////////////////////////////////////////////////////////////
var target = Argument<string>("Target", "Default");
var configuration = Argument<string>("Configuration", "Release");
var outputDirectory = Argument<string>("OutputDirectory", "artifacts");

//Project
var solution = Argument<string>("Solution", "Company.Solution.sln");
//#if (WepApi != "")
var dotnetCoreVersion = Argument<string>("NetCoreVersion", "netcoreapp3.1");
var webapi = Argument<string>("WebApi", "Company.WebApi.csproj");
//#endif

// Folder paths
var paths = new
{
	OutputDirectory = $"./{outputDirectory}"
};

// Versioning
var major = Argument<int>("Major", 1);
var minor = Argument<int>("Minor", 0);
var patch = Argument<int>("Patch", 0);
var runNumber = Argument<int>("RunNumber", 0);
var buildSourceVersion = EnvironmentVariable<string>("Build.SourceVersion", $"{Guid.NewGuid()}");
var isMain = EnvironmentVariable<string>("Build.SourceBranchName","") == "master"; //TODO: Update name of branch

var versioning = new
{
	AssemblyVersion = $"{major}.{minor}.{patch}.{runNumber}",
	InformationalVersion = $"{major}.{minor}.{patch}.{runNumber}+{buildSourceVersion}",
	FileVersion = $"{major}.{minor}.{patch}.{runNumber}",
	PackageVersion = isMain ? $"{major}.{minor}.{patch}" : $"{major}.{minor}.{patch}.{runNumber}"
};

/////////////////////////////////////////////////////////////////
//						TASKS							  	   //
////////////////////////////////////////////////////////////////

Information($"Running target {target} in configuration {configuration}");
// Deletes the contents of the OutputDirectory folder if it contains anything from a previous build.
Task("Clean")
    .Does(() =>
    {
		if(DirectoryExists(paths.OutputDirectory))
		{
			DeleteDirectory(paths.OutputDirectory, new DeleteDirectorySettings {
				Recursive = true,
				Force = true
			});
		}

        DotNetCoreClean(solution);
    });

// Run dotnet restore to restore all package references.
Task("Restore")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

// Versions files and assemblies (excl nuget package version)
Task("Version")
	.Does(() => {
		const string buildPropertiesFile = "./Directory.Build.props";
		Information($"Versioning files");

		XmlPoke(buildPropertiesFile, "//Version", versioning.AssemblyVersion);
		Information($"Version: {versioning.AssemblyVersion}");

		XmlPoke(buildPropertiesFile, "//InformationalVersion", versioning.InformationalVersion);
		Information($"Informational version: {versioning.InformationalVersion}");

		XmlPoke(buildPropertiesFile, "//AssemblyVersion", versioning.AssemblyVersion);
		Information($"Assembly version: {versioning.AssemblyVersion}");

		XmlPoke(buildPropertiesFile, "//FileVersion", versioning.FileVersion);
		Information($"File version: {versioning.FileVersion}");
	});

// Build using the build configuration specified as an argument.
 Task("Build")
    .Does(() =>
    {
        DotNetCoreBuild(solution, new DotNetCoreBuildSettings()
        {
			Configuration = configuration,
			NoRestore = true
        });
    });

Task("Test")
    .Does(() =>
    {
		var projects = GetFiles("**/*.Tests.csproj");
        foreach(var project in projects)
        {
            DotNetCoreTest(
                project.FullPath,
                new DotNetCoreTestSettings()
                {
                    Configuration = configuration,
                    NoBuild = true
                });
        }
    });

//#if (IncludeNuget)
// Pack relevant projects into Nuget packages and output to [OutputDirectory]/packages directory
Task("Pack")
    .Does(() =>
    {
		DotNetCorePack(solution, new DotNetCorePackSettings
        {
            Configuration = configuration,
            NoRestore = true,
            IncludeSymbols = true,
            OutputDirectory = Directory(paths.OutputDirectory + "/packages"),
            MSBuildSettings = new DotNetCoreMSBuildSettings()
                .WithProperty("PackageVersion", versioning.PackageVersion)
        });
    });
//#endif

//#if (WepApi != "")
Task("PublishWeb")
    .Does(() =>
    {
		DotNetCorePublish(webapi,new DotNetCorePublishSettings
		{
			Framework = dotnetCoreVersion,
			Configuration = configuration,
			OutputDirectory = Directory(paths.OutputDirectory + "/Web")
		});
    });
//#endif

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
	.IsDependentOn("Version")
    .IsDependentOn("Build")
	.IsDependentOn("Test")
//#if (IncludeNuget)
	.IsDependentOn("Pack")
//#endif
//#if (WepApi != "")
	.IsDependentOn("PublishWeb")
//#endif
	;

// Executes the task specified in the target argument.
RunTarget(target);