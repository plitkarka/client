using Newtonsoft.Json;
using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.Post;
using Plitkarka.Client.Models.User;
using Plitkarka.Client.Services;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Plitkarka.Client.Repositories;

public class PostClient : MyHttpClient, IPostClient
{
    public PostClient(HttpClient httpClient) : base(httpClient) {}

    public async Task<IdResponse> CreatePost(CreatePostRequest request)
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

        return await GetRequest<IdResponse>(PostHandler.CreatePost(), HttpMethod.Post, content);
    }
    public async Task<object> DeletePost(Guid PostId)
    {
        return await GetRequest<object>(PostHandler.DeletePost(PostId), HttpMethod.Delete);
    }
    public async Task<PaginationResponse<PostResponse>> GetPosts(PaginationIdRequest request)
    {
        return await GetRequest<PaginationResponse<PostResponse>>(PostHandler.GetPosts(request), HttpMethod.Get);
    }
    public async Task<IdResponse> CreatePostLike(Guid PostId)
    {
        return await GetRequest<IdResponse>(PostHandler.TogglePostLike(PostId), HttpMethod.Post);
    }
    public async Task<object> DeletePostLike(Guid PostId)
    {
        return await GetRequest<object>(PostHandler.TogglePostLike(PostId), HttpMethod.Delete);
    }
    public async Task<PaginationResponse<PostResponse>> GetLikedPosts(PaginationIdRequest request)
    {
        return await GetRequest<PaginationResponse<PostResponse>>(PostHandler.GetLikedPosts(request), HttpMethod.Get);
    }
    public async Task<IdResponse> PinPost(Guid PostId)
    {
        return await GetRequest<IdResponse>(PostHandler.TogglePinPost(PostId), HttpMethod.Post);
    }
    public async Task<object> UnpinPost(Guid PostId)
    {
        return await GetRequest<object>(PostHandler.TogglePinPost(PostId), HttpMethod.Delete);
    }
    public async Task<PaginationResponse<PostResponse>> GetPinnedPosts(PaginationIdRequest request)
    {
        return await GetRequest<PaginationResponse<PostResponse>>(PostHandler.GetPinnedPosts(request), HttpMethod.Get);
    }
    public async Task<IdResponse> SharePost(Guid PostId)
    {
        return await GetRequest<IdResponse>(PostHandler.ToggleSharePost(PostId), HttpMethod.Post);
    }
    public async Task<object> DeleteSharedPost(Guid PostId)
    {
        return await GetRequest<object>(PostHandler.ToggleSharePost(PostId), HttpMethod.Delete);
    }
    public async Task<PaginationResponse<PostResponse>> GetSharedPosts(PaginationIdRequest request)
    {
        return await GetRequest<PaginationResponse<PostResponse>>(PostHandler.GetSharedPosts(request), HttpMethod.Get);
    }
}
