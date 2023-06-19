using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.User;

namespace Plitkarka.Client.Interfaces;

public interface ISubscriptionClient
{
    Task<IdResponse> SubscribeAsync(Guid subscribeToId);
    Task UnsubscribeAsync(Guid unsubscribeFromId);
    Task<PaginationResponse<UserPreview>> GetAllSubscribersAsync(PaginationIdRequest request);
    Task<PaginationResponse<UserPreview>> GetAllSuscriptionsAsync(PaginationIdRequest request);
}
