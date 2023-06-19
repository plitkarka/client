namespace Plitkarka.Client.Models.Comments;

public record CreateCommentRequest
{
    public Guid PostId { get; set; }
    public string TextContent { get; set; }
}
