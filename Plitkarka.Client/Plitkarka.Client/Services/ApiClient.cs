using Plitkarka.Client.Interfaces;

namespace Plitkarka.Client.Services;

public class ApiClient: IApiClient
{
    public IUserClient UserClient { get; }

    public ApiClient(IUserClient userClient)
    {
        UserClient = userClient;
    }   
}
