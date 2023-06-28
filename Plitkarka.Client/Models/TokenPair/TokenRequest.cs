namespace Plitkarka.Client.Models.TokenPair;

public record TokenRequest : UniqueIdentifierRequest
{
    public string RefreshToken { get; set; }
}
