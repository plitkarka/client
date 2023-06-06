using Plitkarka.Client.Interfaces;

namespace Plitkarka.Client.Services;

public class ApiClient: IApiClient
{
    public IUserClient UserClient { get; }

    public IAuthClient AuthClient { get; }

    public ApiClient(IUserClient userClient,
        IAuthClient authClient)
    {
        UserClient = userClient;
        AuthClient = authClient;
    }   
}
