namespace Plitkarka.Client.Models.ResetPassword;

public record ResetPasswordRequest
{
    public string Email { get; set; }

    public string PasswordCode { get; set; }

    public string Password { get; set; }

    public string UniqueIdentifier { get; set; }
}
