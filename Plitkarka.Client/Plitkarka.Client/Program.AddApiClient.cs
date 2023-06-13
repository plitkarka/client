using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Repositories;
using Plitkarka.Client.Services;

namespace Plitkarka.Client;

public static partial class Program
{
    public static void AddApiClient(this IServiceCollection services)
    {
        services.AddSingleton<IUserClient, UserClient>();
        services.AddSingleton<IAuthClient, AuthClient>();
        services.AddSingleton<ICommentClient, CommentClient>();
        services.AddSingleton<ISubscriptionClient, SubscriptionClient>();
        services.AddSingleton<IResetPasswordClient, ResetPasswordClient>();
        services.AddSingleton<IApiClient, ApiClient>();
    }
}
