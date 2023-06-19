using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Comments;

namespace Plitkarka.Client.Interfaces;

public interface ICommentClient
{
    Task<IdResponse> CreateCommentAsync(CreateCommentRequest request);
    Task DeleteCommentAsync(Guid id);
    Task<object> GetAllAsync(PaginationIdRequest request);
    Task<IdResponse> CreateCommentLikeAsync(Guid commentId);
    Task DeleteCommentLikeAsync(Guid commentId);
}
