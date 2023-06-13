namespace Plitkarka.Client.Interfaces;

public interface IApiClient
{
    IUserClient UserClient { get; }
    IAuthClient AuthClient { get; }
    ICommentClient CommentClient { get; }
    ISubscriptionClient SubscriptionClient { get; }
    IResetPasswordClient ResetPasswordClient { get; }
}
