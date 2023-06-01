using Plitkarka.Infrastructure.Configurations;

namespace Plitkarka.Client;

public static partial class Program
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
        services
            .AddOptions<HttpClientConfiguration>()
            .BindConfiguration("HttpClientConfiguration")
            .Validate(option =>
                !string.IsNullOrEmpty(option.BaseUrl))
            .ValidateOnStart();

        return services;
    }
}