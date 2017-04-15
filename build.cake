#addin nuget:?package=Cake.Paket
#tool nuget:?package=Paket

var projectName = "EmacsBackend";
var sln = $"{projectName}.sln";
var project = $"{projectName}/{projectName}.fsproj";

Task("Restore").Does(() => {
    PaketRestore(new PaketRestoreSettings {
        Project = project
    });
});

Task("Build").Does(() => {
    DotNetBuild(sln);
});

var target = Argument("target", "default");
RunTarget(target);

// /Users/wk/Source/github/EmacsBackend/EmacsBackend/EmacsBackend/EmacsBackend.fsproj
// /Users/wk/Source/github/EmacsBackend/EmacsBackend/EmacsBackend.fsproj