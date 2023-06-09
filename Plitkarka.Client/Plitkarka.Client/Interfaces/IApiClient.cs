namespace Plitkarka.Client.Interfaces;

public interface IApiClient
{
    IUserClient UserClient { get; }
    IAuthClient AuthClient { get; }
    IResetPasswordClient ResetPasswordClient { get; }
}
