using Plitkarka.Client.Interfaces;

namespace Plitkarka.Client.Services;

public class ApiClient: IApiClient
{
    public IUserClient UserClient { get; }
    public IAuthClient AuthClient { get; }
    public IResetPasswordClient ResetPasswordClient { get; }

    public ApiClient(IUserClient userClient,
        IAuthClient authClient,
        IResetPasswordClient resetPasswordClient)
    {
        UserClient = userClient;
        AuthClient = authClient;
        ResetPasswordClient = resetPasswordClient;
    }   
}
