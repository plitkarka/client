using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Authorization;
using Plitkarka.Infrastructure.Configurations;
using System.Web;

namespace Plitkarka.Client.Services;

public class MyHttpClient
{
    private readonly HttpClient _httpClient;
    public MyHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetRequest<T>(string url,HttpMethod httpMethod,HttpContent? httpContent = null)
    {
        string endpointPath = _httpClient.BaseAddress + url;

        var authToken = JsonConvert.DeserializeObject<TokenPairResponse>(File.ReadAllText(@"tokenpair.json"));
        _httpClient.DefaultRequestHeaders.Remove("AuthToken");

        if (authToken != null && authToken.AccessToken != "")
        {
            _httpClient.DefaultRequestHeaders.Add("AuthToken", authToken.AccessToken);
        }

        HttpResponseMessage response = httpMethod switch
        {
            HttpMethod m when m == HttpMethod.Get => response = await _httpClient.GetAsync(endpointPath),
            HttpMethod m when m == HttpMethod.Post => response = await _httpClient.PostAsync(endpointPath, httpContent),
            HttpMethod m when m == HttpMethod.Put => response = await _httpClient.PutAsync(endpointPath, httpContent),
            HttpMethod m when m == HttpMethod.Delete => response = await _httpClient.DeleteAsync(endpointPath),
            _ => response = await _httpClient.SendAsync(new HttpRequestMessage(httpMethod, endpointPath))
        };
        
        Console.WriteLine("\n" + endpointPath + "\n"+ await response.Content.ReadAsStringAsync());

        if (response.ReasonPhrase=="Accepted" && response.Content.Headers.ContentLength == 0)
        {
            return (T?)(object)await response.Content.ReadAsStringAsync();
        }

        if (((int)response.StatusCode) == StatusCodes.Status500InternalServerError || ((int)response.StatusCode) == StatusCodes.Status403Forbidden)
        {
            //return await response.Content.ReadAsStringAsync();
        }

        if (((int)response.StatusCode) == StatusCodes.Status401Unauthorized)
        {
            await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + "api/auth/refresh"));
        }

        if(response.Content == null) 
        {
            throw new ArgumentException($"The path {endpointPath} gets the following status code: " + response.StatusCode);
        }

        return await response.Content.ReadFromJsonAsync<T>(); // for swagger

        //return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()); // return model
    }
}
