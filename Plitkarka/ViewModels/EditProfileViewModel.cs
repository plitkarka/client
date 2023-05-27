using System.Reactive;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Models;
using Plitkarka.Views;
using ReactiveUI;

namespace Plitkarka.ViewModels;

public class EditProfileViewModel : ReactiveObject
{
    private readonly INavigationService _navigationService;

    public Profile Profile { get; set; }

    public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; }

    public ReactiveCommand<Unit, Unit> CancelChangesCommand { get; }

    public ReactiveCommand<Unit, Unit> EditProfilePhotoCommand { get; }
    
    public EditProfileViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        SaveChangesCommand = ReactiveCommand.CreateFromTask(SaveChanges);
        
        CancelChangesCommand = ReactiveCommand.CreateFromTask(CancelChanges);

        EditProfilePhotoCommand = ReactiveCommand.CreateFromTask(EditProfilePhoto);
    }
    
    private async Task SaveChanges()
    {
        // TODO: add saves later
        await _navigationService.GoBackAsync();
    }
    
    private async Task CancelChanges()
    {
        await _navigationService.GoBackAsync();
    }

    private async Task EditProfilePhoto()
    {
        await _navigationService.NavigateToAsync(nameof(EditProfilePhotoPage));
    }
}