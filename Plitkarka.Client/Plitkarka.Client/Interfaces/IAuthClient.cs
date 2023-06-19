using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Authorization;

namespace Plitkarka.Client.Interfaces;

public interface IAuthClient
{
    Task<TokenPairResponse> GetNewTokenPairAsync(string refreshToken);
    Task<StringResponse> SignUpAsync(SignUpRequest request);
    Task<TokenPairResponse> SignInAsync(SignInRequest request);
    Task<TokenPairResponse> VerifyEmailAsync(VerifyEmailRequest emailRequest);
    Task<StringResponse> ResendVerificationCodeAsync(ResendVerificationCodeRequest email);
    void SignOut();
}
