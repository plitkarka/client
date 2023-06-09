namespace Plitkarka.Client.Models.ResetPassword;

public class VerifyCodeRequest
{
    public string Email { get; set; }
    public string PasswordCode { get; set; }
}
