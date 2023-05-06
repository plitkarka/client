using Plitkarka.Infrastructure.Interfaces;

namespace Plitkarka.Infrastructure.Services;

public class NavigationService : INavigationService
{
    public async Task NavigateToAsync(string route)
    {
        await Shell.Current.GoToAsync($"{route}");
    }

    public async Task GoBackAsync()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}