using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Comments;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.Post;

namespace Plitkarka.Client.Interfaces;

public interface ICommentClient
{
    Task<IdResponse> CreateCommentAsync(CreateCommentRequest request);

    Task DeleteCommentAsync(Guid id);

    Task<PaginationResponse<CommentResponse>> GetAllCommentsAsync(PaginationIdRequest? request = null);

    Task<IdResponse> CreateCommentLikeAsync(Guid commentId);

    Task DeleteCommentLikeAsync(Guid commentId);
}
