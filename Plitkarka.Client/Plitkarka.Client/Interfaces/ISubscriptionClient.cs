using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.User;

namespace Plitkarka.Client.Interfaces;

public interface ISubscriptionClient
{
    Task<IdResponse> Subscribe(Guid subscribeToId);
    Task<object> Unsubscribe(Guid unsubscribeFromId);
    Task<PaginationResponse<UserPreview>> GetAllSubscribers(PaginationIdRequest body);
    Task<PaginationResponse<UserPreview>> GetAllSuscriptions(PaginationIdRequest body);
}
