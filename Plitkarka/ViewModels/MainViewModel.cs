using System.Reactive;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Views;
using ReactiveUI;

namespace Plitkarka.ViewModels;

public class MainViewModel : ReactiveObject
{
    public ReactiveCommand<Unit, Unit> OpenRegistrationFormCommand { get; }
    public ReactiveCommand<Unit, Unit> OpenLoginFormCommand { get; }
    
    public MainViewModel(INavigationService navigationService)
    {
        OpenRegistrationFormCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await navigationService.NavigateToTabAsync(nameof(ProfilePage));
        });

       OpenLoginFormCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await navigationService.NavigateToTabAsync(nameof(EditProfilePage));
        });
    }
}