using Plitkarka.Client.Models;

namespace Plitkarka.Client.Handler;

public class SubscriptionHandler
{
    public static string Subscribe(Guid id)
    {
        return "subscription?SubscribeToId=" + id.ToString();
    }

    public static string UnSubscribe(Guid id)
    {
        return "subscription?UnsubscribeFromId=" + id.ToString();
    }

    public static string GetAllSubscribers(PaginationIdRequest request)
    {
        return request switch
        {
            var res when (request == null || res.Id == Guid.Empty && res.Page == 0) => "subscription/subscribers/all",
            var res when (res.Page == 0) => $"subscription/subscribers/all?Filter={request.Id}",
            var res when (res.Id == Guid.Empty) => $"subscription/subscribers/all?Page={request.Page}",
            _ => $"subscription/subscribers/all?Filter={request.Id}&Page={request.Page}"
        };
    }

    public static string GetAllSuscriptions(PaginationIdRequest request)
    {
        return request switch
        {
            var res when (request == null || res.Id == Guid.Empty && res.Page == 0) => "subscription/subscriptions/all",
            var res when (res.Page == 0) => $"subscription/subscriptions/all?Filter={request.Id}",
            var res when (res.Id == Guid.Empty) => $"subscription/subscriptions/all?Page={request.Page}",
            _ => $"subscription/subscriptions/all?Filter={request.Id}&Page={request.Page}"
        };
    }
}
