﻿using Microsoft.OpenApi.Expressions;
using Newtonsoft.Json;
using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Authorization;
using Plitkarka.Client.Models.ResetPassword;
using Plitkarka.Client.Services;
using System.Text;

namespace Plitkarka.Client.Repositories;

public class ResetPasswordClient : MyHttpClient, IResetPasswordClient
{
    public ResetPasswordClient(HttpClient httpClient) : base(httpClient) {}

    public async Task<StringResponse> SendEmail(SendEmailRequest email)
    {
        var json = JsonConvert.SerializeObject(email);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return await GetRequest<StringResponse>(ResetPasswordHandler.SendEmail(), HttpMethod.Post, content);
    }
    public async Task<VerifyCodeRequest> VerifyCode(VerifyCodeRequest body)
    {
        return await GetRequest<VerifyCodeRequest>(ResetPasswordHandler.VerifyCode(body.Email,body.PasswordCode), HttpMethod.Get);
    }
    public async Task<TokenPairResponse> ResetPassword(ResetPasswordRequest body)
    {
        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return await GetRequest<TokenPairResponse>(ResetPasswordHandler.ResetPassword(), HttpMethod.Put, content);
    }
}
