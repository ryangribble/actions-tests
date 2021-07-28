using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using OctoVersion.Core;
using Nuke.OctoVersion;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    readonly Configuration Configuration = Configuration.Release;

    [Solution] readonly Solution Solution;

    [NukeOctoVersion] readonly OctoVersionInfo OctoVersionInfo;

    static AbsolutePath SourceDirectory => RootDirectory / "source";
    static AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    static AbsolutePath PublishDirectory => RootDirectory / "publish";
    static AbsolutePath LocalPackagesDir => RootDirectory / ".." / "LocalPackages";

    Target CalculateVersion => _ => _
        .Executes(() =>
        {
            //all the magic happens inside `[NukeOctoVersion]` above. we just need a target for TeamCity to call
        });

    Target Default => _ => _
        .DependsOn(CalculateVersion);

    /// Support plugins are available for:
    /// - JetBrains ReSharper        https://nuke.build/resharper
    /// - JetBrains Rider            https://nuke.build/rider
    /// - Microsoft VisualStudio     https://nuke.build/visualstudio
    /// - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Default);
}