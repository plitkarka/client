using System.Reactive;
using Plitkarka.Client.Interfaces;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Stores;
using Plitkarka.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Plitkarka.Extensions;
using Plitkarka.Infrastructure.Helpers;
using Plitkarka.Client.Models.Authorization;

namespace Plitkarka.ViewModels;

public class LoginViewModel : ReactiveObject
{
    private readonly INavigationService _navigationService;
    private readonly IApiClient _apiClient;
    private UserStore _userStore;

    [Reactive] public string Email { get; set; }
    
    [Reactive] public string Password { get; set; }

    [Reactive] public string ErrorText { get; set; }

    public ReactiveCommand<Unit, Unit> LoginCommand { get; }

    public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

    public LoginViewModel(INavigationService navigationService, IApiClient apiClient, UserStore userStore)
    {
        _navigationService = navigationService;
        _apiClient = apiClient;
        _userStore = userStore;

        LoginCommand = ReactiveCommand.CreateFromTask(LoginToProfile);

        GoBackCommand = ReactiveCommand.CreateFromTask(GoBack);
    }

    private async Task LoginToProfile()
    {
        await new AsyncRequestBuilder(async () =>
        {
            var request = new SignInRequest() { Email = Email, Password = Password};
            var test = await _apiClient.BaseApi.AuthClient.SignInAsync(request);
            var user = await _apiClient.BaseApi.UserClient.GetByIdAsync();
            _userStore.CurrentProfile = user.ToViewModel();
        })
        .HandleException<NullReferenceException>(Console.WriteLine)
        .ExecuteAsync();
        
        await _navigationService.NavigateToAsync(nameof(FeedDashboard));
    }

    private async Task GoBack()
    {
        await _navigationService.GoBackAsync();
        Email = Password = string.Empty;
    }
}