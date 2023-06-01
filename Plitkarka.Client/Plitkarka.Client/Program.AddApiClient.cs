using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Repositories;
using Plitkarka.Client.Services;

namespace Plitkarka.Client;

public static partial class Program
{
    public static void AddApiClient(this IServiceCollection services)
    {
        services.AddSingleton<IUserClient, UserClient>();
        services.AddSingleton<IApiClient, ApiClient>();
    }
}
