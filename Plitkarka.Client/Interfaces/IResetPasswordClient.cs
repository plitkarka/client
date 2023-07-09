using Plitkarka.Client.Models.Authorization;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.ResetPassword;

namespace Plitkarka.Client.Interfaces;

public interface IResetPasswordClient
{
    Task<StringResponse> SendEmailAsync(SendEmailRequest email);

    Task<VerifyCodeRequest> VerifyCodeAsync(VerifyCodeRequest request);

    Task<TokenPairResponse> ResetPasswordAsync(ResetPasswordRequest request);
}
