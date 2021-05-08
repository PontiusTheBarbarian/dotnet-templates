/////////////////////////////////////////////////////////////////
//						ARGUMENTS							   //
////////////////////////////////////////////////////////////////
var target = Argument<string>("Target", "Default");
var configuration = Argument<string>("Configuration", "Release");
var outputDirectory = Argument<string>("OutputDirectory", "artifacts");

var assemblyVersion = Argument<string>("AssemblyVersion", "1");
var informationalVersion = Argument<string>("InformationalVersion", "0");
var fileVersion = Argument<string>("FileVersion", "0");
var packageVersion = Argument<string>("PackageVersion", "0");

/////////////////////////////////////////////////////////////////
//						TASKS							  	   //
////////////////////////////////////////////////////////////////

// Deletes the contents of the OutputDirectory folder if it contains anything from a previous build.
Task("Clean")
    .Does(() =>
    {
		if(DirectoryExists($"./{outputDirectory}"))
		{
			DeleteDirectory($"./{outputDirectory}", new DeleteDirectorySettings {
				Recursive = true,
				Force = true
			});
		}

        DotNetCoreClean("_Project_.sln");
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

		XmlPoke(buildPropertiesFile, "//Version", assemblyVersion);
		XmlPoke(buildPropertiesFile, "//InformationalVersion", informationalVersion);
		XmlPoke(buildPropertiesFile, "//AssemblyVersion", assemblyVersion);
		XmlPoke(buildPropertiesFile, "//FileVersion", fileVersion);

		Information($"Version: {assemblyVersion}");
		Information($"Informational version: {informationalVersion}");
		Information($"Assembly version: {assemblyVersion}");
		Information($"File version: {fileVersion}");
	});

// Build using the build configuration specified as an argument.
 Task("Build")
    .Does(() =>
    {
        DotNetCoreBuild("_Project_.sln", new DotNetCoreBuildSettings()
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

Task("Pack")
    .Does(() =>
    {
		DotNetCorePack("_Project_.sln", new DotNetCorePackSettings
        {
            Configuration = configuration,
            NoRestore = true,
            IncludeSymbols = true,
            OutputDirectory = Directory($"./{outputDirectory}/packages"),
            MSBuildSettings = new DotNetCoreMSBuildSettings()
                .WithProperty("PackageVersion", packageVersion)
        });
    });

Task("PublishWeb")
    .Does(() =>
    {
		DotNetCorePublish($"src/_Company_._Project_.WebApi/_Company_._Project_.WebApi.csproj",new DotNetCorePublishSettings
		{
			Framework = "net5.0",
			Configuration = configuration,
			OutputDirectory = Directory($"./{outputDirectory}/_Company_._Project_.WebApi"),
			NoBuild = true
		});
    });

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
	.IsDependentOn("Version")
    .IsDependentOn("Build")
	.IsDependentOn("Test")
	.IsDependentOn("Pack")
	.IsDependentOn("PublishWeb")
	;

// Executes the task specified in the target argument.
RunTarget(target);