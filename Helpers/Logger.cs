using Newtonsoft.Json;

namespace Helpers;
using System;
using System.IO;

public class Logger
{
    private static readonly string logFilePath = "test_log.txt";
    private static readonly object lockObj = new object();

    public static void LogInfo(string message)
    {
        Log("INFO", message);
    }

    public static void LogWarning(string message)
    {
        Log("WARNING", message);
    }

    public static void LogError(string message)
    {
        Log("ERROR", message);
    }

    public static void LogFormattedJson(string rawJson)
    {
        if (string.IsNullOrWhiteSpace(rawJson))
        {
            Console.WriteLine("[JsonLogger] Empty or null JSON string.");
            return;
        }

        try
        {
            var parsedJson = JsonConvert.DeserializeObject(rawJson);
            var prettyJson = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            Console.WriteLine("====== JSON Response ======");
            Console.WriteLine(prettyJson);
            Console.WriteLine("===========================");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"[JsonLogger] Invalid JSON format: {ex.Message}");
            Console.WriteLine("Raw content:");
            Console.WriteLine(rawJson);
        }
    }
    
    private static void Log(string level, string message)
    {
        string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
        
        // Print to Console
        Console.WriteLine(logMessage);

        // Write to File
        lock (lockObj) 
        {
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}
