using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.Post;
using Plitkarka.Client.Services;
using System.Net.Http.Headers;

namespace Plitkarka.Client.Repositories;

public class PostClient : MyHttpClient, IPostClient
{
    public PostClient(HttpClient httpClient) : base(httpClient) {}

    public async Task<IdResponse> CreatePostAsync(CreatePostRequest request)
    {
        var content = new MultipartFormDataContent();
        if (request == null)
        {
            return null;
        }
        if (request.Image != null)
        {
            var streamContent = new StreamContent(request.Image.OpenReadStream());
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            content.Add(streamContent, "Image", "image.png");
        }
        if (request.TextContent != null)
        {
            var stringContent = new StringContent(request.TextContent);
            content.Add(stringContent, "TextContent");
        }

        return await PostRequest<IdResponse>(PostHandler.CreatePost(), content);
    }
    public async Task DeletePostAsync(Guid PostId)
    {
        await DeleteRequest(PostHandler.DeletePost(PostId));
    }
    public async Task<PaginationResponse<PostResponse>> GetPostsAsync(PaginationIdRequest request)
    {
        return await GetRequest<PaginationResponse<PostResponse>>(PostHandler.GetPosts(request));
    }
    public async Task<IdResponse> CreatePostLikeAsync(Guid PostId)
    {
        return await PostRequest<IdResponse>(PostHandler.TogglePostLike(PostId));
    }
    public async Task DeletePostLikeAsync(Guid PostId)
    {
         await DeleteRequest(PostHandler.TogglePostLike(PostId));
    }
    public async Task<PaginationResponse<PostResponse>> GetLikedPostsAsync(PaginationIdRequest request)
    {
        return await GetRequest<PaginationResponse<PostResponse>>(PostHandler.GetLikedPosts(request));
    }
    public async Task<IdResponse> PinPostAsync(Guid PostId)
    {
        return await PostRequest<IdResponse>(PostHandler.TogglePinPost(PostId));
    }
    public async Task UnpinPostAsync(Guid PostId)
    {
        await DeleteRequest(PostHandler.TogglePinPost(PostId));
    }
    public async Task<PaginationResponse<PostResponse>> GetPinnedPostsAsync(PaginationIdRequest request)
    {
        return await GetRequest<PaginationResponse<PostResponse>>(PostHandler.GetPinnedPosts(request));
    }
    public async Task<IdResponse> SharePostAsync(Guid PostId)
    {
        return await PostRequest<IdResponse>(PostHandler.ToggleSharePost(PostId));
    }
    public async Task DeleteSharedPostAsync(Guid PostId)
    {
        await DeleteRequest(PostHandler.ToggleSharePost(PostId));
    }
    public async Task<PaginationResponse<PostResponse>> GetSharedPostsAsync(PaginationIdRequest request)
    {
        return await GetRequest<PaginationResponse<PostResponse>>(PostHandler.GetSharedPosts(request));
    }
}
