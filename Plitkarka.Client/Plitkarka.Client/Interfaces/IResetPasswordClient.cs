using Plitkarka.Client.Models.Authorization;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.ResetPassword;

namespace Plitkarka.Client.Interfaces;

public interface IResetPasswordClient
{
    Task<StringResponse> SendEmail(SendEmailRequest email);
    Task<VerifyCodeRequest> VerifyCode(VerifyCodeRequest request);
    Task<TokenPairResponse> ResetPassword(ResetPasswordRequest request);
}
