using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace UFID_Reader.Services;

public interface IRpiService
{
    string GetRpiSerial();
}

public class RpiService : IRpiService
{
    public string GetRpiSerial()
    {
        // Check if the OS is Windows or MacOS so we can bypass the serial number retrieval during development
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            // Return a default serial number
            return "10000000d340eb60";
        }
        
        // Note: The home/pi directory is the default home directory for the user, currently must be changed to the user of the pi
        CommandSync("cat /proc/cpuinfo | grep Serial | cut -d ':' -f 2 > /home/pi/serial.txt");

        using var sr = new StreamReader("/home/pi/serial.txt");
        return sr.ReadLine().Trim();
    }

    private static void CommandSync(string cmd)
    {
        var info = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"sudo {cmd}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true
        };

        var process = Process.Start(info);
        process.WaitForExit();
    }
}