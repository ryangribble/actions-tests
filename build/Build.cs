using System;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.OctoVersion;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    readonly Configuration Configuration = Configuration.Release;

    [Required]
    [OctoVersion]
    readonly OctoVersionInfo OctoVersionInfo;

    static AbsolutePath SourceDirectory => RootDirectory / "source";
    static AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    static AbsolutePath PublishDirectory => RootDirectory / "publish";
    static AbsolutePath LocalPackagesDir => RootDirectory / ".." / "LocalPackages";

    Target OutputVersion => _ => _
        .Executes(() =>
        {
            //all the magic happens inside `[OctoVersion]` above.  We can just check if the versions got populated here
            Console.WriteLine("Outputting OctoVersion calculated values to see if they were calculated correctly within Nuke");
            Console.WriteLine($"FullSemVer:     {OctoVersionInfo.FullSemVer}");
            Console.WriteLine($"NuGetVersion:   {OctoVersionInfo.NuGetVersion}");
            Console.WriteLine($"BuildMetaData:  {OctoVersionInfo.BuildMetaData}");
        });

    Target Default => _ => _
        .DependsOn(OutputVersion);

    /// Support plugins are available for:
    /// - JetBrains ReSharper        https://nuke.build/resharper
    /// - JetBrains Rider            https://nuke.build/rider
    /// - Microsoft VisualStudio     https://nuke.build/visualstudio
    /// - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Default);
}