using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.User;
using Plitkarka.Client.Services;
using System.Text;

namespace Plitkarka.Client.Repositories;

public class SubscriptionClient : MyHttpClient, ISubscriptionClient
{
    public SubscriptionClient(HttpClient httpClient) : base(httpClient) {}

    public async Task<PaginationResponse<UserPreview>> GetAllSubscribers(PaginationIdRequest body)
    {
        var result = await GetRequest<PaginationResponse<UserPreview>>(SubscriptionHandler.GetAllSubscribers(body), HttpMethod.Get);

        return result;
    }
    public async Task<PaginationResponse<UserPreview>> GetAllSuscriptions(PaginationIdRequest body)
    {
        var result = await GetRequest<PaginationResponse<UserPreview>>(SubscriptionHandler.GetAllSuscriptions(body), HttpMethod.Get);

        return result;
    }
    public async Task<IdResponse> Subscribe(Guid subscribeToId)
    {
        return await GetRequest<IdResponse>(SubscriptionHandler.Subscribe(subscribeToId), HttpMethod.Post);
    }
    public async Task<object> Unsubscribe(Guid unsubscribeFromId)
    {
        return await GetRequest<object>(SubscriptionHandler.UnSubscribe(unsubscribeFromId), HttpMethod.Delete);
    }
}
