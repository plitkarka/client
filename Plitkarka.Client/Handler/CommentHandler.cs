﻿using Plitkarka.Client.Models;

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

    public static string GetAllComments(PaginationIdRequest request)
    {
        return request switch {
            var res when (request == null || res.Id == Guid.Empty && res.Page == 0) => "comments",
            var res when (res.Page == 0) => $"comments?Filter={request.Id}",
            var res when (res.Id == Guid.Empty) => $"comments?Page={request.Page}",
            _ => $"comments?Filter={request.Id}&Page={request.Page}"
        };
    }

    public static string ToggleCommentLike(Guid id)
    {
        return $"comments/like?CommentId={id}";
    }
}
