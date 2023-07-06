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
        return $"user/all?Filter={request.Id}&Page={request.Page}";
    }

    public static string GetAllSuscriptions(PaginationIdRequest request)
    {
        return $"user/all?Filter={request.Id} &Page= {request.Page}";
    }
}
