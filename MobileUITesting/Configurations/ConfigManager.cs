using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = Microsoft.ApplicationInsights.Extensibility.Implementation.JsonSerializer;

namespace MobileUITesting.Configurations;

public class ConfigManager
{
    private static AppSettings _cachedSettings;

    public static AppSettings LoadSettings(string path = "AppConfig.json")
    {
        if (_cachedSettings != null)
            return _cachedSettings;

        if (!File.Exists(path))
            throw new FileNotFoundException($"Configuration file not found at: {path}");

        var json = File.ReadAllText(path);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        _cachedSettings = System.Text.Json.JsonSerializer.Deserialize<AppSettings>(json, options)
                          ?? throw new InvalidOperationException("Could not deserialize appsettings.json");

        foreach (var device in _cachedSettings.Devices)
        {
            if (!Path.IsPathRooted(device.AppPath))
            {
                device.AppPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", device.AppPath));
            }
        }
        
        return _cachedSettings;
    }
}