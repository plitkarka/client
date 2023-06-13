namespace Plitkarka.Client.Models.Comments;

public class CreateCommentRequest
{
    public Guid PostId { get; set; }
    public string TextContent { get; set; }
}
