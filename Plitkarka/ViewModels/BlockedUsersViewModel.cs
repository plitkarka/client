using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Infrastructure;
using Plitkarka.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Plitkarka.ViewModels
{
    public class BlockedUsersViewModel : ReactiveObject
    {
        private readonly INavigationService _navigationService;

        [Reactive] public ObservableCollection<Profile> BlockedUsers { get; set; }

        public ReactiveCommand<Unit, Unit> GoBackCommand { get; set; }

        public ReactiveCommand<Profile, Unit> UnblockUserCommand { get; set; }

        public BlockedUsersViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            BlockedUsers = new ObservableCollection<Profile>
            {
                new() {Nickname = "woooooooooooww"},
                new() {Nickname = "ally.one"},
                new() {Nickname = "lonely_boy"},
                new() {Nickname = "rebellion"},
                new() {Nickname = "seed.of.chaos"}
            };

            GoBackCommand = ReactiveCommand.CreateFromTask(GoBack);

            UnblockUserCommand = ReactiveCommand.Create<Profile>(UnblockUser);
        }

        private void UnblockUser(Profile profile)
        {
            BlockedUsers.Remove(profile);
        }

        private async Task GoBack()
        {
            await _navigationService.GoBackAsync();
        }
    }
}

