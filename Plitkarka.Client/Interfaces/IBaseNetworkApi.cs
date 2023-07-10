namespace Plitkarka.Client.Interfaces;

public interface IBaseNetworkApi
{
    IUserClient UserClient { get; }

    IAuthClient AuthClient { get; }

    IPostClient PostClient { get; }

    ICommentClient CommentClient { get; }

    ISubscriptionClient SubscriptionClient { get; }

    IResetPasswordClient ResetPasswordClient { get; }
}
