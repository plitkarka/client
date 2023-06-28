using Plitkarka.Client.Models.TokenPair;
using System.Web;

namespace Plitkarka.Client.Handler;

public class AuthHandler
{
    public static string GetNewTokenPair(TokenRequest refreshToken)
    {
        return "auth/refresh?refreshToken=" + HttpUtility.UrlEncode(refreshToken.RefreshToken) + "&UniqueIdentifier=" + HttpUtility.UrlEncode(refreshToken.UniqueIdentifier);
    }

    public static string SignUp()
    {
        return "auth";
    }

    public static string SignIn()
    {
        return "auth/signin";
    }

    public static string EmailResponse()
    {
        return "auth/email";
    }
}
