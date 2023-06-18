using Microsoft.Extensions.Options;
using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.User;
using Plitkarka.Client.Services;
using Plitkarka.Infrastructure.Configurations;
using System.Net.Http.Headers;

namespace Plitkarka.Client.Repositories;

public class UserClient : MyHttpClient, IUserClient
{
    public UserClient(HttpClient httpClient) : base(httpClient) {}

    public async Task<IdResponse> SetUserImage(SetUserImageRequestModel image)
    {
        var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(image.Image.OpenReadStream());
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        content.Add(streamContent,"Image","image.png");
        return await GetRequest<IdResponse>(UserHandler.SendUserImage(), HttpMethod.Post, content);
    }
    public async Task<StringResponse> GetImageUrlByUserId(Guid id)
    {
        return await GetRequest<StringResponse>(UserHandler.GetUserImageById(id), HttpMethod.Get);
    }
    public async Task<PaginationResponse<UserPreview>> GetAll(PaginationTextRequest request)
    {
        return await GetRequest<PaginationResponse<UserPreview>>(UserHandler.GetAllUsers(request), HttpMethod.Get);
    }
    public async Task<UserData> GetByIdAsync(Guid id)
    {
        return await GetRequest<UserData>(UserHandler.GetUserById(id), HttpMethod.Get);
    }
}
