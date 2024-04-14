using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebApp.Statics;
namespace WebApp.Helpers;

public class ApiCommunicator(HttpClient httpClient, IConfiguration configuration)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;

    private string CreateApiUri(string? route)
    {
        string apiPath = _configuration["Api:ApiPath"] ?? "";
        string apiKey = _configuration["Api:Secret"] ?? "";

        string uri = apiPath;
        if (!string.IsNullOrEmpty(route))
        {
            string concatPath = (route[0] == '/') ? route.Substring(1) : route;

            uri += "/" + concatPath;
        }

        uri = uri.AddParamToUri($"key={apiKey}");

        return uri;
    }

    public async Task<HttpResponseMessage> PostAsync(string? route, object? value = null)
    {
        string uri = CreateApiUri(route);

        var content = (value != null) 
            ? new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
            : null;

        var response = await _httpClient.PostAsync(uri, content);

        return response;
    }

    public async Task<T?> GetAsync<T>(string? route)
    {
        string uri = CreateApiUri(route);

        var response = await _httpClient.GetAsync(uri);
        var result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

        return result;
    }
}
