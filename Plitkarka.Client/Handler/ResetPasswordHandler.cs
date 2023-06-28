using Plitkarka.Client.Models.ResetPassword;

namespace Plitkarka.Client.Handler;

public class ResetPasswordHandler
{
    public static string SendEmail()
    {
        return "password/reset";
    }

    public static string VerifyCode(VerifyCodeRequest request)
    {
        return $"password/reset?Email={request.Email}&PasswordCode={request.PasswordCode}";
    }

    public static string ResetPassword()
    {
        return "password/reset";
    }
}
