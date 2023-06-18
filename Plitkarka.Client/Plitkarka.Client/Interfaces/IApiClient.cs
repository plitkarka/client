namespace Plitkarka.Client.Interfaces;

public interface IApiClient
{
    IUserClient UserClient { get; }
    IAuthClient AuthClient { get; }
    IPostClient PostClient { get; }
    ICommentClient CommentClient { get; }
    ISubscriptionClient SubscriptionClient { get; }
    IResetPasswordClient ResetPasswordClient { get; }
}
