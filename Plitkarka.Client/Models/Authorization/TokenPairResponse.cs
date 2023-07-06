namespace Plitkarka.Client.Models.Authorization;

public record TokenPairResponse
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}
