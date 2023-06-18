using Plitkarka.Client.Models.Authorization;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Post;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.User;

namespace Plitkarka.Client.Interfaces;

public interface IPostClient
{
    Task<IdResponse> CreatePost(CreatePostRequest request);
    Task<object> DeletePost(Guid PostId);
    Task<PaginationResponse<PostResponse>> GetPosts(PaginationIdRequest request);
    Task<IdResponse> CreatePostLike(Guid PostId);
    Task<object> DeletePostLike(Guid PostId);
    Task<PaginationResponse<PostResponse>> GetLikedPosts(PaginationIdRequest request);
    Task<IdResponse> PinPost(Guid PostId);
    Task<object> UnpinPost(Guid PostId);
    Task<PaginationResponse<PostResponse>> GetPinnedPosts(PaginationIdRequest request);
    Task<IdResponse> SharePost(Guid PostId);
    Task<object> DeleteSharedPost(Guid PostId);
    Task<PaginationResponse<PostResponse>> GetSharedPosts(PaginationIdRequest request);
}
