namespace Plitkarka.Client.Models.ResetPassword;

public class ResetPasswordRequest
{
    public string Email { get; set; }
    public string PasswordCode { get; set; }
    public string Password { get; set; }
}
