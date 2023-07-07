using Plitkarka.Client.Models.User;

namespace Plitkarka.Client.Models.Post;

public record PostResponse
{
    public Guid Id { get; set; }

    public string? TextContent { get; set; }

    public string ImageKey { get; set; }

    public string? ImageUrl { get; set; }

    public int LikesCount { get; set; }

    public int CommentsCount { get; set; }

    public int PinsCount { get; set; }

    public int SharesCount { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsLiked { get; set; }

    public bool IsShared { get; set; }

    public bool IsPinned { get; set; }

    public UserPreview UserPreview { get; set; }
}
