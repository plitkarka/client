using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.User;

namespace Plitkarka.Client.Interfaces;

public interface IUserClient
{
    Task<IdResponse> SetUserImageAsync(SetUserImageRequestModel image);

    Task<StringResponse> GetImageUrlByUserIdAsync(Guid id);

    Task<PaginationResponse<UserPreview>> GetAllAsync(PaginationTextRequest? request = null);

    Task<UserData> GetByIdAsync(Guid id);

    Task<UserData> UpdateUserProfileAsync(UserUpdateProfile request);
}
