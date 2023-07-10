namespace Plitkarka.Client.Models.Authorization;

public record VerifyEmailRequest
{
    public string Email { get; set; }

    public string EmailCode { get; set; }

    public string UniqueIdentifier { get; set; }
}
