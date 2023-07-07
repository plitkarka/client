using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Comments;

namespace Plitkarka.Client.Interfaces;

public interface ICommentClient
{
    Task<IdResponse> CreateCommentAsync(CreateCommentRequest request);

    Task DeleteCommentAsync(Guid id);

    Task<object> GetAllCommentsAsync(PaginationIdRequest? request = null);

    Task<IdResponse> CreateCommentLikeAsync(Guid commentId);

    Task DeleteCommentLikeAsync(Guid commentId);
}
