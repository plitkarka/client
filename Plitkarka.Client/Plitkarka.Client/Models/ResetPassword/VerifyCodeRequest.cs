namespace Plitkarka.Client.Models.ResetPassword;

public record VerifyCodeRequest
{
    public string Email { get; set; }
    public string PasswordCode { get; set; }
}
