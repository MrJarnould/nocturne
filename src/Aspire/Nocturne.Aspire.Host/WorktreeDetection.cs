using System.Diagnostics;

namespace Nocturne.Aspire.Host;

internal enum PersistenceMode { Persistent, Ephemeral }

internal static class WorktreeDetection
{
    /// <summary>
    /// Returns Ephemeral when running from a git worktree (git common dir
    /// differs from git dir) or when NOCTURNE_DB_PERSISTENCE=ephemeral.
    /// Returns Persistent otherwise. Env var wins over auto-detection.
    /// </summary>
    public static PersistenceMode DetectPersistence(string repoRoot)
    {
        var explicitMode = Environment.GetEnvironmentVariable("NOCTURNE_DB_PERSISTENCE");
        if (string.Equals(explicitMode, "persistent", StringComparison.OrdinalIgnoreCase))
            return PersistenceMode.Persistent;
        if (string.Equals(explicitMode, "ephemeral", StringComparison.OrdinalIgnoreCase))
            return PersistenceMode.Ephemeral;

        try
        {
            var commonDir = RunGit(repoRoot, "rev-parse --git-common-dir");
            var gitDir    = RunGit(repoRoot, "rev-parse --git-dir");
            var isWorktree = !string.Equals(
                Path.GetFullPath(commonDir),
                Path.GetFullPath(gitDir),
                StringComparison.OrdinalIgnoreCase);
            return isWorktree ? PersistenceMode.Ephemeral : PersistenceMode.Persistent;
        }
        catch
        {
            return PersistenceMode.Persistent;
        }
    }

    private static string RunGit(string workingDir, string args)
    {
        var psi = new ProcessStartInfo("git", args)
        {
            WorkingDirectory = workingDir,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
        using var p = Process.Start(psi)!;
        var output = p.StandardOutput.ReadToEnd().Trim();
        p.WaitForExit(5_000);
        if (p.ExitCode != 0) throw new InvalidOperationException($"git {args} failed");
        return Path.IsPathRooted(output) ? output : Path.Combine(workingDir, output);
    }
}
