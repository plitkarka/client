using System.Reactive;
using System.Text.RegularExpressions;
using Plitkarka.Infrastructure.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;

namespace Plitkarka.ViewModels
{
    public class ChangePasswordViewModel : ReactiveValidationObject
    {
        private readonly INavigationService _navigationService;
        private const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

        [Reactive] public string CurrentPassword { get; set; }

        [Reactive] public string NewPassword { get; set; }

        [Reactive] public string ConfirmPassword { get; set; }

        [Reactive] public string CurrentPasswordError { get; set; }

        [Reactive] public string NewPasswordError { get; set; }

        [Reactive] public string ConfirmPasswordError { get; set; }

        public ReactiveCommand<Unit, Unit> ChangePasswordCommand { get; }

        public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

        public ChangePasswordViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            var password = "example123";
            this.ValidationRule(vm => vm.CurrentPassword,
                currentPassword => password.Equals(currentPassword),
                "Невірний поточний пароль").ValidationChanged.Subscribe(_ =>
                CurrentPasswordError = _.Text.ToSingleLine());
            
            this.ValidationRule(vm => vm.NewPassword,
                newPassword => CurrentPassword != null && !string.IsNullOrWhiteSpace(newPassword) &&
                               !CurrentPassword.Equals(newPassword) &&
                               Regex.IsMatch(newPassword,
                                   PasswordRegex),
                "Невірний новий пароль").ValidationChanged.Subscribe(_ =>
                NewPasswordError = _.Text.ToSingleLine());
            
            this.ValidationRule(vm => vm.ConfirmPassword,
                confirmPassword => NewPassword != null && !string.IsNullOrWhiteSpace(confirmPassword) &&
                                   NewPassword.Equals(confirmPassword) &&
                                   Regex.IsMatch(confirmPassword,
                                       PasswordRegex),
                "Паролі не співпадають").ValidationChanged.Subscribe(_ =>
                ConfirmPasswordError = _.Text.ToSingleLine());
            
            GoBackCommand = ReactiveCommand.CreateFromTask(GoBack);

            ChangePasswordCommand = ReactiveCommand.CreateFromTask(ChangePassword, this.IsValid());
        }

        private async Task ChangePassword()
        {
            //TODO: add logic for changing password
            await _navigationService.GoBackAsync();
        }

        private async Task GoBack()
        {
            await _navigationService.GoBackAsync();
        }
    }
}