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
      |> MSBuild buildDir "Build" [ "Configuration", "Release"; "RunOctoPack", "true" ]
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

Target "Deploy" (fun _ ->
    !! (buildDir + "*.nupkg")
    |> Seq.iter (fun x -> MoveFile deployDir x)
)

// Dependencies
"Clean"
  ==> "BuildWeb"
  ==> "BuildTest"
  ==> "Test"
  ==> "Deploy"

// start build
RunTargetOrDefault "Deploy"