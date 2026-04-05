using System.IO.Compression;
using System.Text.Json;

namespace Nocturne.DataCommons.Tests;

/// <summary>
/// Reads Nightscout JSON data directly from the OpenAPS Data Commons zip file.
/// Handles both .json and .json.gz entries within the archive.
/// </summary>
public static class NightscoutSampleReader
{
    /// <summary>
    /// Path to the sample data zip, relative to the repository root.
    /// </summary>
    private const string ZipFileName = "2023 - nightscout samples.zip";

    /// <summary>
    /// The 3 user IDs that use Nightscout API format (direct-sharing-31).
    /// The other 5 users use AAPS export format which is not Nightscout API compatible.
    /// </summary>
    private static readonly string[] NightscoutUserIds = ["86025410", "96254963", "74077367"];

    /// <summary>
    /// Finds the zip file by walking up from the test assembly directory to find the repo root.
    /// </summary>
    public static string? FindZipPath()
    {
        // Walk up from the bin directory to find the .data folder
        var dir = AppContext.BaseDirectory;
        for (var i = 0; i < 10; i++)
        {
            var candidate = Path.Combine(dir, ".data", ZipFileName);
            if (File.Exists(candidate))
                return candidate;
            var parent = Directory.GetParent(dir);
            if (parent == null) break;
            dir = parent.FullName;
        }
        return null;
    }

    /// <summary>
    /// Reads and deserializes all JSON files matching a collection name from the zip.
    /// Yields (fileName, records) tuples for each file found.
    /// </summary>
    /// <typeparam name="T">The model type to deserialize (Entry, Treatment, Profile, DeviceStatus)</typeparam>
    /// <param name="zipPath">Path to the zip file</param>
    /// <param name="collectionName">Collection name pattern to match: "entries", "treatments", "profile", "devicestatus"</param>
    public static IEnumerable<(string FileName, T[] Records)> ReadCollection<T>(string zipPath, string collectionName)
    {
        using var zipStream = File.OpenRead(zipPath);
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read);

        foreach (var entry in archive.Entries)
        {
            if (!IsNightscoutFile(entry.FullName, collectionName))
                continue;

            // Skip uncompressed .json files that have a .json.gz equivalent to avoid duplicates
            if (entry.FullName.EndsWith(".json") && !entry.FullName.EndsWith(".json.gz"))
            {
                var gzEquivalent = entry.FullName + ".gz";
                if (archive.GetEntry(gzEquivalent) != null)
                    continue;
            }

            T[]? records;
            using var entryStream = entry.Open();

            if (entry.FullName.EndsWith(".json.gz"))
            {
                using var gzipStream = new GZipStream(entryStream, CompressionMode.Decompress);
                records = JsonSerializer.Deserialize<T[]>(gzipStream);
            }
            else
            {
                records = JsonSerializer.Deserialize<T[]>(entryStream);
            }

            if (records != null && records.Length > 0)
            {
                yield return (entry.FullName, records);
            }
        }
    }

    /// <summary>
    /// Checks if a zip entry path belongs to one of the 3 Nightscout-format users
    /// and matches the requested collection name.
    /// </summary>
    private static bool IsNightscoutFile(string path, string collectionName)
    {
        // Must be from one of the 3 Nightscout users
        if (!NightscoutUserIds.Any(id => path.StartsWith(id + "/")))
            return false;

        // Must be a JSON file
        if (!path.EndsWith(".json") && !path.EndsWith(".json.gz"))
            return false;

        // Must contain the collection name in the filename (not directory)
        var fileName = Path.GetFileName(path);
        return fileName.Contains(collectionName, StringComparison.OrdinalIgnoreCase);
    }
}
