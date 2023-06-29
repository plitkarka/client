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
    public ResetPasswordClient() : base() {}

    public async Task<StringResponse> SendEmailAsync(SendEmailRequest email)
    {
        var json = JsonConvert.SerializeObject(email);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return await PostRequest<StringResponse>(ResetPasswordHandler.SendEmail(), content);
    }

    public async Task<VerifyCodeRequest> VerifyCodeAsync(VerifyCodeRequest request)
    {
        return await GetRequest<VerifyCodeRequest>(ResetPasswordHandler.VerifyCode(request));
    }

    public async Task<TokenPairResponse> ResetPasswordAsync(ResetPasswordRequest request)
    {
        request.UniqueIdentifier = await DeviceIdService.GetDeviceRegistrationId();
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var tokenPair = await PutRequest<TokenPairResponse>(ResetPasswordHandler.ResetPassword(), content);

        await JsonServices.SerializeToFileAsync(FileHandler.GetTokenPairFileLocation(), JsonConvert.SerializeObject(tokenPair, Formatting.Indented));

        return tokenPair;
    }
}
