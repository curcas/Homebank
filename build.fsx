#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Testing

// Properties
let buildDir = "./build/"
let testDir  = "./test/"
let deployDir = "./deploy/"

RestorePackages()

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir; deployDir]
)

Target "BuildWeb" (fun _ ->
    !! "src/web/**/*.csproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "Output: "
)

Target "BuildTest" (fun _ ->
    !! "src/tests/**/*.csproj"
      |> MSBuildDebug testDir "Build"
      |> Log "Output: "
)

Target "Test" (fun _ ->
    !! (testDir + "/Homebank.*.Tests.dll")
      |> xUnit2 (fun p ->
          {p with HtmlOutputPath = Some(testDir @@ "TestResults.html") 
                  ToolPath = "packages/xunit.runner.console/tools/xunit.console.exe"
          })
)

Target "Package" (fun _ ->
    !! (buildDir + "_PublishedWebsites/Homebank.Web/**/*.*")
        |> Zip (buildDir + "_PublishedWebsites/Homebank.Web") (deployDir + "Homebank.Web.zip")
)

// Dependencies
"Clean"
  ==> "BuildWeb"
  ==> "BuildTest"
  ==> "Test"
  ==> "Package"

// start build
RunTargetOrDefault "Package"