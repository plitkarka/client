namespace Plitkarka.Client.Models.User;

public record User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
