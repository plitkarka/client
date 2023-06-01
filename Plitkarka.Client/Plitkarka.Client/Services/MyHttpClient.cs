using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Plitkarka.Client.Models;
using Plitkarka.Infrastructure.Configurations;

namespace Plitkarka.Client.Services;

public class MyHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly HttpClientConfiguration _httpClientConfiguration;
    public MyHttpClient(IOptions<HttpClientConfiguration> httpClientConfiguration)
    {
        _httpClient = new HttpClient();
        _httpClientConfiguration = httpClientConfiguration.Value;
    }

    public async Task<T> GetRequest<T>(string url,HttpMethod httpMethod,HttpContent? httpContent = null)
    {
        string endpointPath = _httpClientConfiguration.BaseUrl + url;


        /*
        HttpRequestMessage request = new HttpRequestMessage(httpMethod, endpointPath);
        HttpResponseMessage response = await _httpClient.SendAsync(request);
        */

        HttpResponseMessage response = httpMethod.Method switch
        {
            "GET" => response = await _httpClient.GetAsync(endpointPath),
            "POST" => response = await _httpClient.PostAsync(endpointPath, httpContent),
            "PUT" => response = await _httpClient.PutAsync(endpointPath, httpContent),
            "DELETE" => response = await _httpClient.DeleteAsync(endpointPath),
            _ => response = await _httpClient.SendAsync(new HttpRequestMessage(httpMethod, endpointPath))
        };
        Console.WriteLine("\n" + endpointPath + "\n");
        if (((int)response.StatusCode) == StatusCodes.Status500InternalServerError)
        {
            throw new ArgumentException($"Server Internal Error");
        }

        if (((int)response.StatusCode) == StatusCodes.Status401Unauthorized)
        {
            await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, _httpClientConfiguration.BaseUrl + "api/auth/refresh"));
        }

        if(response.Content == null) 
        {
            throw new ArgumentException($"The path {endpointPath} gets the following status code: " + response.StatusCode);
        }
        return await response.Content.ReadFromJsonAsync<T>();

        //return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }
}
