namespace Plitkarka.Infrastructure.Interfaces;

public interface INavigationService
{
    Task NavigateToAsync(string route);
    Task NavigateToTabAsync(string route);
    Task GoBackAsync();
}
