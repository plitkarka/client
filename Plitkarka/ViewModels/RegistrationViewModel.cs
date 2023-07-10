using System.Reactive;
using ReactiveUI;
using Plitkarka.Views;
using ReactiveUI.Fody.Helpers;
using Plitkarka.Infrastructure.Interfaces;

using Validator = Plitkarka.Infrastructure.Helpers.ValidationHelper;
using Plitkarka.Client.Interfaces;
using Plitkarka.Infrastructure.Helpers;
using Plitkarka.Client.Models.Authorization;
using Plitkarka.Stores;
using Plitkarka.Extensions;

namespace Plitkarka.ViewModels;

public class RegistrationViewModel : ReactiveObject
{
    private readonly INavigationService _navigationService;
    private readonly IApiClient _apiClient;
    private UserStore _userStore;

    [Reactive] public string Name { get; set; }

    [Reactive] public DateTime BirthDate { get; set; }

    [Reactive] public string Nickname { get; set; }

    [Reactive] public string Email { get; set; }

    [Reactive] public string Password { get; set; }

    [Reactive] public string ConfirmPassword { get; set; }

    [Reactive] public string ErrorText { get; set; }

    public ReactiveCommand<Unit, Unit> RegisterCommand { get; }
    
    [Reactive] public bool IsValid { get; set; }
    
    public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

    public RegistrationViewModel(INavigationService navigationService, IApiClient apiClient, UserStore userStore)
    {
        _navigationService = navigationService;
        _apiClient = apiClient;
        _userStore = userStore;

        RegisterCommand = ReactiveCommand.CreateFromTask(Register);

        GoBackCommand = ReactiveCommand.CreateFromTask(GoBack);
    }

    private async Task Register()
    {
        await new AsyncRequestBuilder(async () =>
        {
            Console.WriteLine(Nickname);
            //var request = new SignUpRequest() { Email = Email, Login = Nickname, Name = "Тарас Дерденчук", Password = "123", RepeatPassword = "123", BirthDate = DateTime.Now };
            //var auth = await _apiClient.BaseApi.AuthClient.SignUpAsync(request);
            var confirm = await _apiClient.BaseApi.AuthClient.VerifyEmailAsync(new VerifyEmailRequest() { Email = "Mrdurden@gmail.com", EmailCode = "189901" });
            var user = await _apiClient.BaseApi.UserClient.GetByIdAsync();
            _userStore.CurrentProfile = user.ToViewModel();
        }).ExecuteAsync();

        IsValid = true;

        await _navigationService.NavigateToAsync(nameof(FeedDashboard));
    }

    private async Task GoBack()
    {
        await _navigationService.GoBackAsync();
        Nickname = Email = Password = string.Empty;
    }
}