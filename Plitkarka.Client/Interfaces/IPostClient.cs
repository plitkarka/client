using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Post;
using Plitkarka.Client.Models.Pagination;

namespace Plitkarka.Client.Interfaces;

public interface IPostClient
{
    Task<IdResponse> CreatePostAsync(CreatePostRequest request);

    Task DeletePostAsync(Guid PostId);

    Task<PaginationResponse<PostResponse>> GetPostsAsync(PaginationIdRequest request);

    Task<PaginationResponse<PostResponse>> GetMediaPostsAsync(PaginationIdRequest request);

    Task<PaginationResponse<PostResponse>> GetFeedAsync(PaginationRequest request);

    Task<PaginationResponse<PostResponse>> SearchPostsAsync(PaginationTextRequest request);

    Task<IdResponse> CreatePostLikeAsync(Guid PostId);

    Task DeletePostLikeAsync(Guid PostId);

    Task<PaginationResponse<PostResponse>> GetLikedPostsAsync(PaginationIdRequest request);

    Task<IdResponse> PinPostAsync(Guid PostId);

    Task UnpinPostAsync(Guid PostId);

    Task<PaginationResponse<PostResponse>> GetPinnedPostsAsync(PaginationIdRequest request);

    Task<IdResponse> SharePostAsync(Guid PostId);

    Task DeleteSharedPostAsync(Guid PostId);

    Task<PaginationResponse<PostResponse>> GetSharedPostsAsync(PaginationIdRequest request);
}
