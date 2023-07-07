using Plitkarka.Client.Models.User;

namespace Plitkarka.Client.Models.Comments;

public record CommentResponse
{
    public Guid CommentId { get; set; }

    public string TextContent { get; set; }

    public int LikesCount { get; set; }

    public bool Isliked { get; set; }

    public DateTime CreateDate { get; set; }

    public UserPreview UserPreview { get; set; }
}
