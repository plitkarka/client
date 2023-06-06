namespace Plitkarka.Client.Models.Authorization;

public record class SignUpRequest
{
    public string Login { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
    public DateTime BirthDate { get; set; }
}
