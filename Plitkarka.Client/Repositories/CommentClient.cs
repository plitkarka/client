using Newtonsoft.Json;
using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Comments;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Services;
using System.Text;

namespace Plitkarka.Client.Repositories;

public class CommentClient : MyHttpClient, ICommentClient
{
    public CommentClient(HttpClient httpClient) : base(httpClient) {}

    public async Task<IdResponse> CreateCommentAsync(CreateCommentRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return await PostRequest<IdResponse>(CommentHandler.CreateComment(), content);
    }

    public async Task DeleteCommentAsync(Guid id)
    {
        await DeleteRequest(CommentHandler.DeleteComment(id));
    }

    public async Task<PaginationResponse<CommentResponse>> GetAllCommentsAsync(PaginationIdRequest? request = null)
    {
        return await GetRequest<PaginationResponse<CommentResponse>>(CommentHandler.GetAllComments(request));
    }

    public async Task<IdResponse> CreateCommentLikeAsync(Guid commentId)
    {
        return await PostRequest<IdResponse>(CommentHandler.ToggleCommentLike(commentId));
    }

    public async Task DeleteCommentLikeAsync(Guid commentId)
    {
        await DeleteRequest(CommentHandler.ToggleCommentLike(commentId));
    }
}
