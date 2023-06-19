namespace Plitkarka.Client.Models.User;

public record UserPreview : User
{
    public string ImageKey { get; set; }
    public string ImageUrl { get; set; }
}
