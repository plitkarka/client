using Newtonsoft.Json;
using Plitkarka.Client.Handler;
using Plitkarka.Client.Models.Authorization;
using System.Net;

namespace Plitkarka.Client.Services;

public class MyHttpClient
{
    private readonly HttpClient _httpClient;
    public MyHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetRequest<T>(string url)
    {
        var authToken = await CheckAuthToken();

        var response = await _httpClient.GetAsync(CreateRequestURL(url));

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            GetNewAuthTokenPair(authToken);
            await GetRequest<T>(url);
        }

        CheckStatusCode(response);

        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }
    public async Task<T?> PostRequest<T>(string url, HttpContent? httpContent = null)
    {
        var authToken = await CheckAuthToken();

        var response = await _httpClient.PostAsync(CreateRequestURL(url), httpContent);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            GetNewAuthTokenPair(authToken);
            await PostRequest<T>(url, httpContent);
        }

        CheckStatusCode(response);

        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }
    public async Task<T?> PutRequest<T>(string url, HttpContent? httpContent = null)
    {
        var authToken = await CheckAuthToken();

        var response = await _httpClient.PutAsync(CreateRequestURL(url), httpContent);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            GetNewAuthTokenPair(authToken);
            await PutRequest<T>(url, httpContent);
        }

        CheckStatusCode(response);

        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }
    public async Task DeleteRequest(string url)
    {
        var authToken = await CheckAuthToken();

        var response = await _httpClient.DeleteAsync(CreateRequestURL(url));

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            GetNewAuthTokenPair(authToken);
            await DeleteRequest(url);
        }

        CheckStatusCode(response);
    }

    private async Task<TokenPairResponse> CheckAuthToken()
    {
        var authToken = await JsonServices.DeserializeFromFileAsync<TokenPairResponse>("tokenpair.json");
        _httpClient.DefaultRequestHeaders.Remove("AuthToken");

        if (authToken != null && authToken.AccessToken != "")
        {
            _httpClient.DefaultRequestHeaders.Add("AuthToken", authToken.AccessToken);
        }

        return authToken;
    }
    private string CreateRequestURL(string url) => _httpClient.BaseAddress + url;
    private async void GetNewAuthTokenPair(TokenPairResponse authTokens)
    {
        var authToken = await CheckAuthToken();
        var response = await _httpClient.GetAsync(CreateRequestURL(AuthHandler.GetNewTokenPair(authToken.RefreshToken)));
        
        if (response.IsSuccessStatusCode)
        {
            var tokenPair = JsonConvert.DeserializeObject<TokenPairResponse>(await response.Content.ReadAsStringAsync());
            JsonServices.SerializeToFileAsync("tokenpair.json", JsonConvert.SerializeObject(tokenPair, Formatting.Indented));
        }
    }
    private void CheckStatusCode(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.InternalServerError || response.StatusCode == HttpStatusCode.Forbidden)
        {
            throw new Exception("Some errors occured on the server");
        }
    }
}
