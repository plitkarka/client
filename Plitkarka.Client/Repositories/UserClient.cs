using Newtonsoft.Json;
using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.User;
using Plitkarka.Client.Services;
using System.Net.Http.Headers;
using System.Text;

namespace Plitkarka.Client.Repositories;

public class UserClient : MyHttpClient, IUserClient
{
    public UserClient() : base() {}

    public async Task<IdResponse> SetUserImageAsync(SetUserImageRequestModel image)
    {
        var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(image.Image.OpenReadStream());
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        content.Add(streamContent,"Image","image.png");

        return await PostRequest<IdResponse>(UserHandler.SendUserImage(), content);
    }

    public async Task<StringResponse> GetImageUrlByUserIdAsync(Guid id)
    {
        return await GetRequest<StringResponse>(UserHandler.GetUserImageById(id));
    }

    public async Task<PaginationResponse<UserPreview>> GetAllAsync(PaginationTextRequest request)
    {
        return await GetRequest<PaginationResponse<UserPreview>>(UserHandler.GetAllUsers(request));
    }

    public async Task<UserData> GetByIdAsync(Guid id)
    {
        return await GetRequest<UserData>(UserHandler.GetUserById(id));
    }

    public async Task<UserData> UpdateUserProfileAsync(UserUpdateProfile request)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return await PutRequest<UserData>(UserHandler.UpdateUserProfile(), content);
    }
}
