using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.User;

namespace Plitkarka.Client.Interfaces;

public interface IUserClient
{
    Task<IdResponse> SetUserImage(SetUserImageRequestModel image);
    Task<StringResponse> GetImageUrlByUserId(Guid id);
    Task<PaginationResponse<UserPreview>> GetAll(PaginationTextRequest body);
    Task<UserData> GetByIdAsync(Guid id);
}
