using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Authorization;

namespace Plitkarka.Client.Interfaces;

public interface IAuthClient
{
    Task<TokenPairResponse> GetNewTokenPair(string refreshToken);
    Task<StringResponse> SignUp(SignUpRequest body);
    Task<TokenPairResponse> SignIn(SignInRequest body);
    Task<TokenPairResponse> VerifyEmail(VerifyEmailRequest emailBody);
    Task<StringResponse> ResendVerificationCode(ResendVerificationCodeRequest email);
}
