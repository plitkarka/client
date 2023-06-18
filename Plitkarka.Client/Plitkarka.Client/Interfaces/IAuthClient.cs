using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Authorization;

namespace Plitkarka.Client.Interfaces;

public interface IAuthClient
{
    Task<TokenPairResponse> GetNewTokenPair(string refreshToken);
    Task<StringResponse> SignUp(SignUpRequest request);
    Task<TokenPairResponse> SignIn(SignInRequest request);
    void SignOut();
    Task<TokenPairResponse> VerifyEmail(VerifyEmailRequest emailRequest);
    Task<StringResponse> ResendVerificationCode(ResendVerificationCodeRequest email);
}
