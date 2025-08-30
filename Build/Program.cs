using GlobExpressions;
using static Bullseye.Targets;
using static SimpleExec.Command;

namespace Build;

internal static class Program
{
    internal static async Task Main(string[] args)
    {
        const string clean = "clean";
        const string build = "build";
        const string test = "test";
        const string format = "format";
        const string publish = "publish";

        Target(
            clean,
            ["publish", "**/bin", "**/obj"],
            dir =>
            {
                IEnumerable<string> GetDirectories(string d) => Glob.Directories(".", d);

                foreach (var d in GetDirectories(dir))
                {
                    RemoveDirectory(d);
                }

                return;

                void RemoveDirectory(string d)
                {
                    if (!Directory.Exists(d))
                    {
                        return;
                    }

                    Console.WriteLine($"Cleaning {d}");
                    Directory.Delete(d, true);
                }
            }
        );

        Target(
            format,
            () =>
            {
                Run("dotnet", "tool restore");
                Run("dotnet", "csharpier format .");
            }
        );

        Target(build, [format],
            () => Run($"dotnet", $"build {GetSolutionFolderPath()} -c Release"));

        Target(
            test,
            [build],
            () =>
            {
                IEnumerable<string> GetFiles(string d) => Glob.Files(".", d);

                foreach (var file in GetFiles("tests/**/*.csproj"))
                {
                    Run(
                        "dotnet",
                        $"test {file} -c Release --no-restore --no-build --verbosity=normal"
                    );
                }
            }
        );

        Target(
            publish,
            [test],
            [$"{GetSolutionFolderPath()}/SimpleClinicApi"],
            project =>
                // Console.WriteLine($"Current directory: {Directory.GetCurrentDirectory()}");
                Run(
                    "dotnet",
                    $"publish {project} -c Release -f net9.0 -o ./publish --no-restore --no-build --verbosity=normal"
                ));

        Target("default", [publish], () => Console.WriteLine("Done!"));
        await RunTargetsAndExitAsync(args);
    }

    private static string GetSolutionFolderPath()
    {
        // Get the directory where the currently executing assembly is located
        var currentProjectDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Traverse up the directory tree
        var directory = new DirectoryInfo(currentProjectDirectory);

        while (directory != null && !HasSolutionFile(directory.FullName))
        {
            directory = directory.Parent;
        }

        return directory?.FullName ?? throw
            // Solution file not found, handle this case as needed
            new FileNotFoundException("Solution file not found in parent directories.");
    }

    private static bool HasSolutionFile(string directoryPath)
    {
        var solutionFiles = Directory.GetFiles(directoryPath, "*.sln");
        return solutionFiles.Length > 0;
    }
}
