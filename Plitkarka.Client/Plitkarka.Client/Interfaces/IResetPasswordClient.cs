using Plitkarka.Client.Models.Authorization;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.ResetPassword;

namespace Plitkarka.Client.Interfaces;

public interface IResetPasswordClient
{
    Task<StringResponse> SendEmail(SendEmailRequest email);
    Task<VerifyCodeRequest> VerifyCode(VerifyCodeRequest body);
    Task<TokenPairResponse> ResetPassword(ResetPasswordRequest body);
}
