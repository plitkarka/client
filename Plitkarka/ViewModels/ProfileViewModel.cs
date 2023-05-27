using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Models;
using Plitkarka.Views;

namespace Plitkarka.ViewModels
{
    public class ProfileViewModel : ReactiveObject
    {
        private readonly INavigationService _navigationService;
        
        [Reactive] public Profile Profile { get; set; }
        
        [Reactive] public ObservableCollection<Segment> Segments { get; set; }

        [Reactive] public ObservableCollection<Profile> Recommendations { get; set; }
        
        [Reactive] public ObservableCollection<Post> Posts { get; set; }

        public ReactiveCommand<int,Unit> FollowCommand { get; }

        public ReactiveCommand<Segment,Unit> ChangeContentCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenEditPageCommand { get; }

        public ReactiveCommand<Unit, Unit> GoBackCommand { get; }
        
        public ProfileViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
            Profile = new Profile
            {
                Name = "Олександр Петров", Nickname = "@OleksandrPetrov", Bio = "Ви не фотографуєте, ви створюєте!",
                Followers = 212, Following = 71, UserId = 1
            };
            
            Recommendations = new ObservableCollection<Profile>
            {
                new Profile { UserId = 1, Nickname = "user1", PhotoUrl = "https://example.com/user1.jpg" },
                new Profile { UserId = 2, Nickname = "user2", PhotoUrl = "https://example.com/user2.jpg" },
                new Profile { UserId = 3, Nickname = "user3", PhotoUrl = "https://example.com/user2.jpg" },
                new Profile { UserId = 4, Nickname = "user4", PhotoUrl = "https://example.com/user2.jpg" },

                // Add more profiles as needed
            };

            Segments = new ObservableCollection<Segment>
            {
                new Segment("Плітки"),
                new Segment("Репліти"),
                new Segment("Медіа"),
                new Segment("Вподобайки"),
            };

            Posts = new ObservableCollection<Post>
            {
                new Post{ AuthorProfileImage = "https://example.com/user1.jpg", AuthorName = "LinaBoyko_HR", PostDate = DateTime.Now, PostContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec euismod, nisl eget aliquam ultricies, nunc nisl ultricies nunc, quis aliquam nisl nisl eu nisl. Sed euismod, nisl eget aliquam ultricies, nunc nisl ultricies nunc, quis aliquam nisl nisl eu nisl.", LikesCount = 10, CommentsCount = 5 , ReplitsCount = 2, SavesCount = 5, SharesCount = 1},
                new Post{ AuthorProfileImage = "https://example.com/user2.jpg", AuthorName = "LinaBoyko_HR", PostDate = DateTime.Now, PostContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec euismod, nisl eget aliquam ultricies, nunc nisl ultricies nunc, quis aliquam nisl nisl eu nisl. Sed euismod, nisl eget aliquam ultricies, nunc nisl ultricies nunc, quis aliquam nisl nisl eu nisl.", LikesCount = 10, CommentsCount = 5 , ReplitsCount = 4, SavesCount = 2, SharesCount = 1},
                new Post{ AuthorProfileImage = "https://example.com/user3.jpg", AuthorName = "LinaBoyko_HR", PostDate = DateTime.Now, PostContent = "Example of content", CommentsCount = 500, LikesCount = 20000, ReplitsCount = 450, SavesCount = 200, SharesCount = 100}
            };

            ChangeContentCommand = ReactiveCommand.Create<Segment>(ChangeContent);

            FollowCommand = ReactiveCommand.CreateFromTask<int>(async _ =>
            {
                var userId = 123;
                await FollowUser(userId);
            });

            OpenEditPageCommand = ReactiveCommand.Create(() =>
            {
                _navigationService.NavigateToAsync(nameof(EditProfilePage));
            });

            GoBackCommand = ReactiveCommand.Create(() =>
            {
                _navigationService.GoBackAsync();
            });
                        
            Segments[0].IsSelected = true;
        }

        private void ChangeContent(Segment segment)
        {
            foreach (var seg in Segments)
            {
                seg.IsSelected = (seg == segment);
                seg.SegmentContent = seg.SegmentName;
            }
        }

        private async Task FollowUser(int userId)
        {
            await Task.Delay(1000);
        }
    }
}