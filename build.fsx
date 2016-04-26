#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Testing

// Properties
let buildDir = "build"
let testDir  = "test"
let deployDir = "deploy"

// Helpers
let GetEnvironmentVariableOrDefault (name : string) (defaultValue : string) = (
    let result = environVar name
    if result <> null then result else defaultValue
)

let GetBuildNumber = GetEnvironmentVariableOrDefault "BUILD_NUMBER" "0"

RestorePackages()

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir; deployDir]
)

Target "BuildWeb" (fun _ ->
    !! ("src" @@ "web" @@ "**" @@ "*.csproj")
      |> MSBuild buildDir "Build" [ "Configuration", "Release" ]
      |> Log "Output: "
)

Target "BuildTest" (fun _ ->
    !! ("src" @@ "tests" @@ "**" @@ "*.csproj")
      |> MSBuildDebug testDir "Build"
      |> Log "Output: "
)

Target "Test" (fun _ ->
    !! (testDir @@ "Homebank.*.Tests.dll")
      |> xUnit2 (fun p ->
          {p with HtmlOutputPath = Some(testDir @@ "TestResults.html") 
                  ToolPath = "packages" @@ "xunit.runner.console" @@ "tools" @@ "xunit.console.exe"
          })
)

Target "Deploy" (fun _ ->
    NuGet (fun p -> 
        {p with
            Authors = ["Curdin Caspar"]
            Project = "Homebank"
            Description = "Homebank Release"
            Version = "2016." + GetBuildNumber
            OutputPath = deployDir
            WorkingDir = "."
            Files = [
                    (buildDir @@ "_PublishedWebsites" @@ "**" @@ "*.*", None, None)
            ]
        })
        "template.nuspec"
)

// Dependencies
"Clean"
  ==> "BuildWeb"
  ==> "BuildTest"
  ==> "Test"
  ==> "Deploy"

// start build
RunTargetOrDefault "Deploy"