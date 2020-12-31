@REM Setup script: 
@REM Creates base directorys where source code and tests will live,
@REM restores dotnet sdk related tools such as 'build.cake', recursively
@REM searches current directory for '.csproj' files and adds them to the 
@REM solution file (will list out connected projects after running) and runs
@REM the cicd process.
@REM 
@REM * Can be run multiple times


@REM Create default directories if they dont already exist
IF NOT EXIST "src" mkdir "src"
IF NOT EXIST "tests" mkdir "tests"

@REM Recursively searches currenty directory for csproj files to add to the solution
For /R %%G in (*.csproj) do dotnet sln Company.Solution.sln add "%%G"

@REM Lists out currently connected projects
dotnet sln list

@REM Installs dotnet tools from manifest
IF EXIST ".config" dotnet tool restore

@REM Runs cake.build
IF EXIST "build.cake" dotnet cake

@REM Pause execution to allow user to validate output
@REM pause