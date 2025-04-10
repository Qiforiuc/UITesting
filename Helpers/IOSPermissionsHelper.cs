using System.Diagnostics;

namespace Helpers;

public class IOSPermissionsHelper
{
    public static void GrantPermission(string bundleId, string service)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "xcrun",
            Arguments = $"simctl privacy booted grant {service} {bundleId}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (var process = new Process { StartInfo = startInfo })
        {
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Console.WriteLine($"Error granting permission '{service}' to '{bundleId}': {error}");
            }
            else
            {
                Console.WriteLine($"Granted '{service}' permission to '{bundleId}': {output}");
            }
        }
    }

    public static void GrantAllPermissions(string bundleId)
    {
        string[] services = {
            "camera", "microphone", "location", "photos", "contacts",
            "calendar", "reminders", "motion", "media-library"
        };

        foreach (var service in services)
        {
            GrantPermission(bundleId, service);
        }
    }
}