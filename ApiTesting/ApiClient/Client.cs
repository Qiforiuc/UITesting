using ApiTesting.Commons;
using Helpers;
using RestSharp;
using Newtonsoft.Json;


namespace ApiTesting.ApiClient;
public class Client
{
    private readonly string _baseUrl;
    private readonly Context _context;

    public Client(string baseUrl)
    {
        _baseUrl = baseUrl;
        _context = Context.Instance;
    }

    public T GetRequest<T>(string endpoint)
    {
        var client = new RestClient(_baseUrl);
        var request = new RestRequest(endpoint, Method.Get);

        var response = client.Execute(request);
        Logger.LogInfo($"[GET] {_baseUrl + endpoint} - Status: {response.StatusCode}");
        Logger.LogFormattedJson(response.Content);
        T result = JsonConvert.DeserializeObject<T>(response.Content);
        return result;
    }
}