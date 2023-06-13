using Plitkarka.Client.Interfaces;

namespace Plitkarka.Client.Services;

public class ApiClient: IApiClient
{
    public IUserClient UserClient { get; }
    public IAuthClient AuthClient { get; }
    public ICommentClient CommentClient { get; }
    public IResetPasswordClient ResetPasswordClient { get; }
    public ISubscriptionClient SubscriptionClient { get; }

    public ApiClient(IUserClient userClient,
        IAuthClient authClient,
        ICommentClient commentClient,
        IResetPasswordClient resetPasswordClient,
        ISubscriptionClient subscriptionClient)
    {
        UserClient = userClient;
        AuthClient = authClient;
        CommentClient = commentClient;
        ResetPasswordClient = resetPasswordClient;
        SubscriptionClient = subscriptionClient;
    }   
}
