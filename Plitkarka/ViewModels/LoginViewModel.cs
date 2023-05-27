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
  
    public ReactiveCommand<Unit, Task> LoginCommand { get; }

    public LoginViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        LoginCommand = ReactiveCommand.Create(async () =>
        {
            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
            {
                ErrorText = Strings.InvalidEmail;
                return;
            }

            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
            {
                ErrorText = Strings.InvalidEmail;
                return;
            }

            await _navigationService.NavigateToTabAsync(nameof(HomePage));
        });
    }
}
