using Plitkarka.Client.Models;
using System.Web;

namespace Plitkarka.Client.Handler;

public class CommentHandler
{
    public static string CreateComment()
    {
        return "comments";
    }
    public static string DeleteComment(Guid id)
    {
        return $"comments?CommentId={id}";
    }
    public static string GetAll(PaginationIdRequest body)
    {
        return body switch {
            var res when (res.Id == Guid.Empty && res.Page == 0) => "comments",
            var res when (res.Page == 0) => $"comments?Filter={body.Id}",
            var res when (res.Id == Guid.Empty) => $"comments?Page={body.Page}",
            _ => $"comments?Filter={body.Id}&Page={body.Page}"
        };
    }
    public static string ToggleCommentLike(Guid id)
    {
        return $"comments/like?CommentId={id}";
    }
}
