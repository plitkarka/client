using System.Web;

namespace Plitkarka.Client.Handler;

public class ResetPasswordHandler
{
    public static string SendEmail()
    {
        return "password/reset";
    }
    public static string VerifyCode(string email, string passwordCode)
    {
        return $"password/reset?Email={email}&PasswordCode={passwordCode}";
    }
    public static string ResetPassword()
    {
        return "password/reset";
    }
}
