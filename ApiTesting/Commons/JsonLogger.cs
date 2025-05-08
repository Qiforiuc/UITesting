using Newtonsoft.Json;

namespace ApiTesting.Commons;


public static class JsonLogger
{
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
}