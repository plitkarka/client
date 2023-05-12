using System.Reactive;
using System.Resources;
using ReactiveUI;
using Plitkarka.Infrastructure.StringResources;
using Plitkarka.Views;
using Plitkarka.Infrastructure.Helpers;
using ReactiveUI.Fody.Helpers;
using Plitkarka.Infrastructure.Interfaces;

namespace Plitkarka.ViewModels;

public class RegistrationViewModel : ReactiveObject
{
    private readonly INavigationService _navigationService;

    [Reactive] public string Name { get; set; }

    [Reactive] public string Surname { get; set; }

    [Reactive] public DateTime BirthDate { get; set; }

    [Reactive] public string Email { get; set; }

    [Reactive] public string Password { get; set; }

    [Reactive] public string ConfirmPassword { get; set; }

    [Reactive] public string ErrorText { get; set; }

    public ReactiveCommand<Unit, Task> RegisterCommand { get; }

    public RegistrationViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        RegisterCommand = ReactiveCommand.Create(async () =>
        {
            var validationRules = new Dictionary<string, Func<bool>>
            {
                { nameof(Name), () => ValidationHelper.IsNameValid(Name) },
                { nameof(Surname), () => ValidationHelper.IsSurnameValid(Surname) },
                { nameof(Email), () => ValidationHelper.IsEmailValid(Email) },
                { nameof(Password), () => ValidationHelper.IsPasswordValid(Password) },
                { nameof(ConfirmPassword), () => ValidationHelper.IsPasswordConfirmed(Password, ConfirmPassword) },
                { nameof(BirthDate), () => ValidationHelper.IsBirthDateValid(BirthDate)}
            };

            foreach (var rule in validationRules)
            {
                if (!rule.Value())
                {
                    ErrorText = Strings.ResourceManager.GetString("Invalid" + rule.Key);
                    return;
                }
            }

            await _navigationService.NavigateToAsync(nameof(HomePage));
        });
    }
}
