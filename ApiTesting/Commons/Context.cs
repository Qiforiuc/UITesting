using System.Reflection;
using Helpers;

namespace ApiTesting.Commons;

public sealed class Context
{
    private static readonly Lazy<Context> _instance =
        new Lazy<Context>(() => new Context());

    private readonly Dictionary<string, object> _contextData;

    private Context()
    {
        _contextData = new Dictionary<string, object>();
        LoadPropertiesFromFile("api.properties");
    }

    public static Context Instance => _instance.Value;

    public void SetValue(string key, object value)
    {
        _contextData[key] = value;
    }

    public object GetValue(string key)
    {
        _contextData.TryGetValue(key, out var value);
        return value;
    }

    private void LoadPropertiesFromFile(string fileName)
    {
        try
        {
            var fullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            if (!File.Exists(fullPath))
            {
                Logger.LogError($"Sorry, unable to find {fileName}");
                return;
            }

            foreach (var line in File.ReadAllLines(fullPath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                    continue;

                var split = line.Split('=', 2);
                if (split.Length == 2)
                {
                    SetValue(split[0].Trim(), split[1].Trim());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading properties: {ex.Message}");
        }
    }
}