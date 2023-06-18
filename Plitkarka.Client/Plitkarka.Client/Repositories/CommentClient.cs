using Newtonsoft.Json;
using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Comments;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Services;
using System.ComponentModel.Design;
using System.Text;

namespace Plitkarka.Client.Repositories;

public class CommentClient : MyHttpClient, ICommentClient
{
    public CommentClient(HttpClient httpClient) : base(httpClient) {}

    public async Task<IdResponse> CreateComment(CreateCommentRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return await GetRequest<IdResponse>(CommentHandler.CreateComment(), HttpMethod.Post, content);
    }
    public async Task<object> DeleteComment(Guid id)
    {
        return await GetRequest<object>(CommentHandler.DeleteComment(id), HttpMethod.Delete);
    }
    public async Task<object> GetAll(PaginationIdRequest request)
    {
        return await GetRequest<object>(CommentHandler.GetAll(request), HttpMethod.Get);
    }
    public async Task<IdResponse> CreateCommentLike(Guid commentId)
    {
        return await GetRequest<IdResponse>(CommentHandler.ToggleCommentLike(commentId), HttpMethod.Post);
    }
    public async Task<object> DeleteCommentLike(Guid commentId)
    {
        return await GetRequest<IdResponse>(CommentHandler.ToggleCommentLike(commentId), HttpMethod.Delete);
    }
}
