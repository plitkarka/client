namespace Plitkarka.Client.Models.Authorization;

public record ResendVerificationCodeRequest
{
    public string Email { get; set; }
}
