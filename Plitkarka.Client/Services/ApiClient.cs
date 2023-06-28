using Plitkarka.Client.Interfaces;

namespace Plitkarka.Client.Services;

public class ApiClient : IApiClient
{
    public IBaseNetworkApi BaseApi { get; }

    public ApiClient(IBaseNetworkApi baseApi)
    {
        BaseApi = baseApi;
    }
}
