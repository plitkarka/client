﻿using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Authorization;
using Plitkarka.Client.Services;
using System.Text;
using Newtonsoft.Json;

namespace Plitkarka.Client.Repositories;

public class AuthClient : MyHttpClient, IAuthClient
{
    public AuthClient(HttpClient httpClient) : base(httpClient) {}

    public async Task<TokenPairResponse> GetNewTokenPairAsync(string refreshToken)
    {
        var tokenPair = await GetRequest<TokenPairResponse>(AuthHandler.GetNewTokenPair(refreshToken));

        await JsonServices.SerializeToFileAsync("tokenpair.json", JsonConvert.SerializeObject(tokenPair, Formatting.Indented));

        return tokenPair;
    }
    public async Task<StringResponse> ResendVerificationCodeAsync(ResendVerificationCodeRequest email)
    {
        var json = JsonConvert.SerializeObject(email);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return await PutRequest<StringResponse>(AuthHandler.EmailResponse(), content);
    }
    public async Task<TokenPairResponse> SignInAsync(SignInRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var tokenPair = await PostRequest<TokenPairResponse>(AuthHandler.SignIn(), content);

        await JsonServices.SerializeToFileAsync("tokenpair.json", JsonConvert.SerializeObject(tokenPair, Formatting.Indented));

        return tokenPair;
    }
    public async Task<StringResponse> SignUpAsync(SignUpRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        SignOut();

        return await PostRequest<StringResponse>(AuthHandler.SignUp(), content);
    }
    public async Task<TokenPairResponse> VerifyEmailAsync(VerifyEmailRequest emailRequest)
    {
        var json = JsonConvert.SerializeObject(emailRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var tokenPair = await PostRequest<TokenPairResponse>(AuthHandler.EmailResponse(), content);

        await JsonServices.SerializeToFileAsync("tokenpair.json", JsonConvert.SerializeObject(tokenPair, Formatting.Indented));

        return tokenPair;
    }
    public async void SignOut()
    {
        await JsonServices.ClearFileAsync("tokenpair.json");
    }
}
