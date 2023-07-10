using System.Reactive;
using Plitkarka.Models;
using Plitkarka.Views;
using Plitkarka.Infrastructure.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Plitkarka.Infrastructure.Helpers;
using Plitkarka.Client.Interfaces;
using Plitkarka.Stores;
using Plitkarka.Client.Models.User;

namespace Plitkarka.ViewModels;

public class EditProfileViewModel : ReactiveObject
{
    private readonly INavigationService _navigationService;
    private readonly IMessagingService _messagingService;
    private readonly IApiClient _apiClient;
    private UserStore _userStore;

    [Reactive] public Profile Profile { get; set; }

    [Reactive] public string Name { get; set; }

    [Reactive] public string Nickname { get; set; }

    [Reactive] public string Bio { get; set; }

    [Reactive] public string Link { get; set; }

    [Reactive] public string PhotoUrl { get; set; }

    public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; }

    public ReactiveCommand<Unit, Unit> CancelChangesCommand { get; }

    public ReactiveCommand<Unit, Unit> EditProfilePhotoCommand { get; }
    
    public EditProfileViewModel(INavigationService navigationService, IMessagingService messagingService, IApiClient apiClient, UserStore userStore)
    {
        _navigationService = navigationService;
        _messagingService = messagingService;
        _apiClient = apiClient;

        _userStore = userStore;

        Profile = _userStore.CurrentProfile;
        
        SaveChangesCommand = ReactiveCommand.CreateFromTask(SaveChanges);
        
        CancelChangesCommand = ReactiveCommand.CreateFromTask(CancelChanges);

        EditProfilePhotoCommand = ReactiveCommand.CreateFromTask(EditProfilePhoto);
    }

    private async Task SaveChanges()
    {
        Profile.Name = string.IsNullOrWhiteSpace(Name) ? Profile.Name : Name;
        Profile.Nickname = string.IsNullOrWhiteSpace(Nickname) ? Profile.Nickname : Nickname;
        Profile.Bio = string.IsNullOrWhiteSpace(Bio) ? Profile.Bio : Bio;
        Profile.Link = string.IsNullOrWhiteSpace(Link) ? Profile.Link : Link;
        Profile.PhotoUrl = string.IsNullOrWhiteSpace(PhotoUrl) ? Profile.PhotoUrl : PhotoUrl;

        await new AsyncRequestBuilder(async () =>
        {
            var request = new UserUpdateProfile() { Login = Nickname, Name = Name, Description = Bio, Link = Link };
            var user = await _apiClient.BaseApi.UserClient.UpdateUserProfileAsync(request);
        }).ExecuteAsync();

        Name = Nickname = Bio = Link = string.Empty;

        _userStore.CurrentProfile = Profile;

        await _navigationService.GoBackAsync();
    }
    
    private async Task CancelChanges() => await _navigationService.GoBackAsync();

    private async Task EditProfilePhoto()
    {
        await new AsyncRequestBuilder(async () =>
        {
            var image = await MediaPicker.PickPhotoAsync(new MediaPickerOptions());
            if (image is not null)
            {
                var newFile = Path.Combine(FileSystem.CacheDirectory, image.FileName);
                using (var stream = await image.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                PhotoUrl = newFile;
            };
        })
        .HandleException<OperationCanceledException>(e => Console.WriteLine(e.Message))
        .ExecuteAsync();
    }
}