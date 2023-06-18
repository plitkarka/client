using Microsoft.Extensions.Options;
using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Authorization;
using Plitkarka.Client.Services;
using Plitkarka.Infrastructure.Configurations;
using System.Text;
using System;
using System.Net.WebSockets;
using Newtonsoft.Json;

namespace Plitkarka.Client.Repositories;

public class AuthClient : MyHttpClient, IAuthClient
{
    public AuthClient(HttpClient httpClient) : base(httpClient) {}

    public async Task<TokenPairResponse> GetNewTokenPair(string refreshToken)
    {
        var tokenPair = await GetRequest<TokenPairResponse>(AuthHandler.GetNewTokenPair(refreshToken), HttpMethod.Get);

        Services.JsonSerializer.SerializeToFile("tokenpair.json", JsonConvert.SerializeObject(tokenPair, Formatting.Indented));

        return tokenPair;
    }
    public async Task<StringResponse> ResendVerificationCode(ResendVerificationCodeRequest email)
    {
        var json = JsonConvert.SerializeObject(email);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return await GetRequest<StringResponse>(AuthHandler.EmailResponse(), HttpMethod.Put, content);
    }
    public async Task<TokenPairResponse> SignIn(SignInRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var tokenPair = await GetRequest<TokenPairResponse>(AuthHandler.SignIn(), HttpMethod.Post, content);

        Services.JsonSerializer.SerializeToFile("tokenpair.json", JsonConvert.SerializeObject(tokenPair, Formatting.Indented));

        return tokenPair;
    }
    public async Task<StringResponse> SignUp(SignUpRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        SignOut();

        return await GetRequest<StringResponse>(AuthHandler.SignUp(), HttpMethod.Post, content);
    }
    public async Task<TokenPairResponse> VerifyEmail(VerifyEmailRequest emailRequest)
    {
        var json = JsonConvert.SerializeObject(emailRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var tokenPair = await GetRequest<TokenPairResponse>(AuthHandler.EmailResponse(), HttpMethod.Post, content);

        Services.JsonSerializer.SerializeToFile("tokenpair.json", JsonConvert.SerializeObject(tokenPair, Formatting.Indented));

        return tokenPair;
    }
    public void SignOut()
    {
        Services.JsonSerializer.ClearFile("tokenpair.json");
    }

}
