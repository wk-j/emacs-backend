#addin nuget:?package=Cake.Paket
#tool nuget:?package=Paket

var projectName = "EmacsBackend";
var sln = $"{projectName}.sln";
var project = $"{projectName}/{projectName}.fsproj";

Action<string,string> process = (cmd, args) => {
    StartProcess(cmd, new ProcessSettings {
        Arguments = args
    });
};

Task("Restore").Does(() => {
    PaketRestore(new PaketRestoreSettings {
        Project = project
    });
});

Task("Build").Does(() => {
    MSBuild(sln);
});

Task("Run")
    .IsDependentOn("Build")
    .Does(() => {
            var path = $"{projectName}/bin/Debug/{projectName}.exe";
            process("mono", path);
     });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
    var path = $"{projectName}.Tests/bin/Debug/{projectName}.Tests.exe";
    process("mono", path);
});

var target = Argument("target", "default");
RunTarget(target);

// /Users/wk/Source/github/EmacsBackend/EmacsBackend/EmacsBackend/EmacsBackend.fsproj
// /Users/wk/Source/github/EmacsBackend/EmacsBackend/EmacsBackend.fsproj
