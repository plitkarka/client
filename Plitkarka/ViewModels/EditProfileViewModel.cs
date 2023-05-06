using System.Reactive;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Models;
using ReactiveUI;

namespace Plitkarka.ViewModels;

public class EditProfileViewModel : ReactiveObject
{
    private readonly INavigationService _navigationService;

    public Profile Profile { get; set; }

    public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; }

    public ReactiveCommand<Unit, Unit> CancelChangesCommand { get; }
    
    public EditProfileViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        SaveChangesCommand = ReactiveCommand.Create(SaveChanges);
        
        CancelChangesCommand = ReactiveCommand.Create(CancelChanges);
    }
    
    private void SaveChanges()
    {
        // TODO: add saves later
        _navigationService.GoBackAsync();
    }
    
    private void CancelChanges()
    {
        _navigationService.GoBackAsync();
    }
}