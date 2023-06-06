namespace Plitkarka.Client.Models.Authorization;

public record SignInRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
