namespace Plitkarka.Infrastructure.Interfaces;

public interface INavigationService
{
    Task NavigateToAsync(string route);
    Task GoBackAsync();
}
