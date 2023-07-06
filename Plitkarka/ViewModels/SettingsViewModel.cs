using System.Reactive;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Views;
using ReactiveUI;

namespace Plitkarka.ViewModels;

public class SettingsViewModel : ReactiveObject
{
    private readonly INavigationService _navigationService;

    public ReactiveCommand<Unit,Unit> OpenBlockedUsersPageCommand { get; set; }

    public ReactiveCommand<Unit, Unit> OpenChangePasswordPageCommand { get; set; }

    public ReactiveCommand<Unit, Unit> GoBackCommand { get; set; }

    public SettingsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        OpenBlockedUsersPageCommand = ReactiveCommand.CreateFromTask(OpenBlockedUsersPage);

        OpenChangePasswordPageCommand = ReactiveCommand.CreateFromTask(OpenChangePasswordPage);

        GoBackCommand = ReactiveCommand.CreateFromTask(GoBack);
    }

    private async Task GoBack()
    {
        await _navigationService.GoBackAsync();
    }

    private async Task OpenChangePasswordPage()
    {
        await _navigationService.NavigateToAsync(nameof(ChangePasswordPage));
    }

    private async Task OpenBlockedUsersPage()
    {
        await _navigationService.NavigateToAsync(nameof(BlockedUsersPage));
    }
}