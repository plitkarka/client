using Newtonsoft.Json;
using Plitkarka.Client.Configurations;
using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models.Authorization;
using Plitkarka.Client.Models.TokenPair;
using System.Net;
using System.Net.Http;

namespace Plitkarka.Client.Services;

public class MyHttpClient : IHttpClient
{
    private readonly HttpClient _httpClient;
    public MyHttpClient(HttpClient httpClient)
    {
        var config = JsonConvert.DeserializeObject<HttpConfiguration>(File.ReadAllText("httpclientconfig.json"));
        _httpClient = new HttpClient() { BaseAddress = new Uri(config.BaseUrl) };
    }

    public async Task<T?> GetRequest<T>(string url)
    {
        await CheckAuthToken();

        var response = await _httpClient.GetAsync(CreateRequestURL(url));

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (await GetNewAuthTokenPair())
            {
                await GetRequest<T>(url);
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        CheckStatusCode(response);

        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<T?> PostRequest<T>(string url, HttpContent? httpContent = null)
    {
        await CheckAuthToken();

        var response = await _httpClient.PostAsync(CreateRequestURL(url), httpContent);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (await GetNewAuthTokenPair())
            {
                await PostRequest<T>(url, httpContent);
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        CheckStatusCode(response);

        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<T?> PutRequest<T>(string url, HttpContent? httpContent = null)
    {
        await CheckAuthToken();

        var response = await _httpClient.PutAsync(CreateRequestURL(url), httpContent);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (await GetNewAuthTokenPair())
            {
                await PutRequest<T>(url, httpContent);
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        CheckStatusCode(response);

        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task PutRequest(string url, HttpContent? httpContent = null)
    {
        await CheckAuthToken();

        var response = await _httpClient.PutAsync(CreateRequestURL(url), httpContent);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (await GetNewAuthTokenPair())
            {
                await PutRequest(url, httpContent);
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        CheckStatusCode(response);
    }

    public async Task DeleteRequest(string url)
    {
        await CheckAuthToken();

        var response = await _httpClient.DeleteAsync(CreateRequestURL(url));

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (await GetNewAuthTokenPair())
            {
                await DeleteRequest(url);
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        CheckStatusCode(response);
    }

    private async Task<TokenPairResponse> CheckAuthToken()
    {
        var authToken = await JsonServices.DeserializeFromFileAsync<TokenPairResponse>(FileHandler.GetTokenPairFileLocation());
        _httpClient.DefaultRequestHeaders.Remove("AuthToken");

        if (authToken != null && authToken.AccessToken != "")
        {
            _httpClient.DefaultRequestHeaders.Add("AuthToken", authToken.AccessToken);
        }

        return authToken;
    }

    private string CreateRequestURL(string url) => _httpClient.BaseAddress + url;

    private async Task<bool> GetNewAuthTokenPair()
    {
        var authToken = await CheckAuthToken();
        var token = new TokenRequest() { RefreshToken = authToken.RefreshToken, UniqueIdentifier = await DeviceIdService.GetDeviceRegistrationId() };
        var response = await _httpClient.GetAsync(CreateRequestURL(AuthHandler.GetNewTokenPair(token)));

        Console.WriteLine(await response.Content.ReadAsStringAsync());
        if (response.IsSuccessStatusCode)
        {
            var tokenPair = JsonConvert.DeserializeObject<TokenPairResponse>(await response.Content.ReadAsStringAsync());
            await JsonServices.SerializeToFileAsync(FileHandler.GetTokenPairFileLocation(), JsonConvert.SerializeObject(tokenPair, Formatting.Indented));
            return true;
        }
        else
        {
            await JsonServices.ClearFileAsync(FileHandler.GetTokenPairFileLocation());
            return false;
        }
    }

    private async void CheckStatusCode(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            throw new Exception("Some errors occured on the server");
        }
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            throw new Exception("No content");
        }
    }
}

