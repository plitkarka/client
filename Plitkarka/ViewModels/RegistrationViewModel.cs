using System.Reactive;
using System.Resources;
using ReactiveUI;
using Plitkarka.Infrastructure.StringResources;
using Plitkarka.Views;
using ReactiveUI.Fody.Helpers;
using Plitkarka.Infrastructure.Interfaces;
using ReactiveUI.Fody.Helpers;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Infrastructure.Helpers;

using Validator = Plitkarka.Infrastructure.Helpers.ValidationHelper;

namespace Plitkarka.ViewModels;

public class RegistrationViewModel : ReactiveObject
{
    private readonly INavigationService _navigationService;

    [Reactive] public string Name { get; set; }

    [Reactive] public string Surname { get; set; }

    [Reactive] public DateTime BirthDate { get; set; }

    [Reactive] public string Nickname { get; set; }

    [Reactive] public string Email { get; set; }

    [Reactive] public string Password { get; set; }

    [Reactive] public string ConfirmPassword { get; set; }

    [Reactive] public string ErrorText { get; set; }

    public ReactiveCommand<Unit, Task> RegisterCommand { get; }
    
    [Reactive] public bool IsValid { get; set; }
    
    public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

    [Reactive] public bool IsValid { get; set; }

    public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

    public RegistrationViewModel(INavigationService navigationService)

    {
        _navigationService = navigationService;

        this.WhenAnyValue(vm => vm.Email)
                .Subscribe(email =>
                {
                    if (!string.IsNullOrWhiteSpace(email) && !Validator.IsEmailValid(email))
                        ErrorText = "Invalid email";
                });

        this.WhenAnyValue(vm => vm.Password)
            .Subscribe(password =>
            {
                if (!string.IsNullOrWhiteSpace(password) && !Validator.IsPasswordValid(password))
                    ErrorText = "Invalid password";
            });

        RegisterCommand = ReactiveCommand.Create(async () =>
        {
            //var validationRules = new Dictionary<string, Func<bool>>
            //{
            //    { nameof(Email), () => ValidationHelper.IsEmailValid(Email) },
            //    { nameof(Password), () => ValidationHelper.IsPasswordValid(Password) },
            //    { nameof(ConfirmPassword), () => ValidationHelper.IsPasswordConfirmed(Password, ConfirmPassword) }
            //};

            //foreach (var rule in validationRules)
            //{
            //    if (!rule.Value())
            //    {
            //        ErrorText = Strings.ResourceManager.GetString("Invalid" + rule.Key);
            //        IsValid = false;
            //        return;
            //    }
            //}

            IsValid = true;

            await _navigationService.NavigateToTabAsync(nameof(HomePage));
        });

        GoBackCommand = ReactiveCommand.CreateFromTask(GoBack);
    }

    private async Task GoBack()
    {
        await _navigationService.GoBackAsync();
        Nickname = Email = Password = string.Empty;
    }
}