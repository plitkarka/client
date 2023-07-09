using System.Reactive;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Infrastructure.StringResources;
using Plitkarka.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Plitkarka.ViewModels;

public class LoginViewModel : ReactiveObject
{
    private readonly INavigationService _navigationService;

    [Reactive] public string Email { get; set; }

    [Reactive] public string Password { get; set; }

    [Reactive] public string ErrorText { get; set; }

    public ReactiveCommand<Unit, Unit> LoginCommand { get; }

    public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

    public LoginViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        LoginCommand = ReactiveCommand.CreateFromTask(LoginToProfile);

        GoBackCommand = ReactiveCommand.CreateFromTask(GoBack);
    }

    private async Task LoginToProfile()
    {
        await _navigationService.NavigateToAsync(nameof(FeedDashboard));
    }

    private async Task GoBack()
    {
        await _navigationService.GoBackAsync();
        Email = Password = string.Empty;
    }
}