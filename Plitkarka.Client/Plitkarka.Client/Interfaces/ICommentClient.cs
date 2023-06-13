using Plitkarka.Client.Models.Authorization;
using Plitkarka.Client.Models.ResetPassword;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Comments;
using Plitkarka.Client.Models.User;
using Plitkarka.Client.Models.Pagination;

namespace Plitkarka.Client.Interfaces;

public interface ICommentClient
{
    Task<IdResponse> CreateComment(CreateCommentRequest body);
    Task<object> DeleteComment(Guid id);
    Task<object> GetAll(PaginationIdRequest body);
    Task<IdResponse> CreateCommentLike(Guid commentId);
    Task<object> DeleteCommentLike(Guid commentId);
}
