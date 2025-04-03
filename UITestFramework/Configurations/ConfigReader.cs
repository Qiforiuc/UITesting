using Newtonsoft.Json;

namespace UITestFramework.Configurations;

public class ConfigReader
{
    public static AppConfig LoadConfig(string path = "/Users/admin/RiderProjects/UITestFramework/UITestFramework/Configurations/AppConfig.json")
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Configuration file not found: {path}");
        }
        
        string jsonString = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<AppConfig>(jsonString);
    }
}