namespace Plitkarka.Client.Models.Post;

public record CreatePostRequest
{
    public IFormFile? Image { get; set; }
    public string? TextContent { get; set; }
}
