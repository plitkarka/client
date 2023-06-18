namespace Plitkarka.Client.Models.Authorization;

public class TokenPairResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
