using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.User;
using Plitkarka.Client.Services;

namespace Plitkarka.Client.Repositories;

public class SubscriptionClient : MyHttpClient, ISubscriptionClient
{
    public SubscriptionClient() : base() {}

    public async Task<PaginationResponse<UserPreview>> GetAllSubscribersAsync(PaginationIdRequest request)
    {
        return await GetRequest<PaginationResponse<UserPreview>>(SubscriptionHandler.GetAllSubscribers(request));
    }

    public async Task<PaginationResponse<UserPreview>> GetAllSuscriptionsAsync(PaginationIdRequest request)
    {
        return await GetRequest<PaginationResponse<UserPreview>>(SubscriptionHandler.GetAllSuscriptions(request));
    }

    public async Task<IdResponse> SubscribeAsync(Guid subscribeToId)
    {
        return await PostRequest<IdResponse>(SubscriptionHandler.Subscribe(subscribeToId));
    }

    public async Task UnsubscribeAsync(Guid unsubscribeFromId)
    {
        await DeleteRequest(SubscriptionHandler.UnSubscribe(unsubscribeFromId));
    }
}
