using System.Text.Json;
using Newtonsoft.Json;

namespace MobileUITesting.Configurations;

public class ConfigManager
{
    public static AppSettings LoadSettings(string path = "C:\\Users\\Adrian\\RiderProjects\\UITesting\\MobileUITesting\\AppConfig.json")
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Configuration file not found: {path}");
        }
        
        var jsonString = File.ReadAllText(path);
        if (string.IsNullOrEmpty(jsonString))
        {
            throw new InvalidOperationException("JSON string is null or empty");
        }

        return JsonConvert.DeserializeObject<AppSettings>(jsonString) ?? throw new InvalidOperationException("Deserialized object is null");
    }
}