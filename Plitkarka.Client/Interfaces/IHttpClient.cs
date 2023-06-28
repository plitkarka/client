namespace Plitkarka.Client.Interfaces;

public interface IHttpClient
{
    Task<T?> GetRequest<T>(string url);

    Task<T?> PostRequest<T>(string url, HttpContent? httpContent = null);

    Task<T?> PutRequest<T>(string url, HttpContent? httpContent = null);

    Task DeleteRequest(string url);
}
