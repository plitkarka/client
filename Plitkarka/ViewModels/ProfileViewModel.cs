using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Models;
using Plitkarka.Stores;
using Plitkarka.Views;
using Plitkarka.Client.Interfaces;
using Plitkarka.Infrastructure.Helpers;
using Plitkarka.Client.Models;
using Plitkarka.Extensions;

namespace Plitkarka.ViewModels
{
    public class ProfileViewModel : ReactiveObject
    {
        private readonly INavigationService _navigationService;
        private readonly IMessagingService _messagingService;
        private readonly IApiClient _apiClient;
        private readonly UserStore _userStore;
        private readonly PostStore _postStore;

        [Reactive] public bool IsRecommendationsVisible { get; set; }

        [Reactive] public Profile Profile { get; set; }

        [Reactive] public Profile UserProfile { get; set; }

        [Reactive] public ObservableCollection<Segment> Segments { get; set; }

        [Reactive] public ObservableCollection<Profile> Recommendations { get; set; }

        [Reactive] public ObservableCollection<Post> Posts { get; set; }

        [Reactive] public ObservableCollection<Post> UserPosts { get; set; }

        [Reactive] public ObservableCollection<Post> ReplitedPosts { get; set; }

        public ReactiveCommand<Guid, Unit> FollowCommand { get; }

        public ReactiveCommand<Segment, Unit> ChangeContentCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenEditPageCommand { get; }

        public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenCreatePostPageCommand { get; }

        public ReactiveCommand<Unit, Unit> CloseRecommendationsCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenSettingsPageCommand { get; }

        public ReactiveCommand<Post, Unit> LikePostCommand { get; }

        public ReactiveCommand<Post, Unit> CommentPostCommand { get; }

        public ReactiveCommand<Post, Unit> SavePostCommand { get; }

        public ReactiveCommand<Unit, Unit> SubscribeCommand { get;}

        public ProfileViewModel(INavigationService navigationService, IMessagingService messagingService, IApiClient apiClient, UserStore userStore, PostStore postStore)
        {
            _navigationService = navigationService;
            _messagingService = messagingService;
            _apiClient = apiClient;
            _userStore = userStore;
            _postStore = postStore;

            Subscribe();

            Profile = _userStore.CurrentProfile;
            
            Recommendations = new ObservableCollection<Profile>
            {
                new Profile { Nickname = "vasilii", PhotoUrl = "https://st2.depositphotos.com/1037987/9820/i/950/depositphotos_98204712-stock-photo-man-having-breakfast-in-a.jpg" },
                new Profile { Nickname = "aminamina", PhotoUrl = "https://img.freepik.com/premium-photo/attractive-young-woman-sitting-alone-near-big-window-coffee-shop-table-with-cup-cappuccino-cake-relaxing-restaurant-during-free-time-young-female-having-rest-cafe-lifestyle-concept_365776-12533.jpg?w=2000" },
                new Profile { Nickname = "katya.l", PhotoUrl = "https://d2gg9evh47fn9z.cloudfront.net/800px_COLOURBOX11110978.jpg" },
            };

            Segments = new ObservableCollection<Segment>
            {
                new("Плітки"),
                new("Репліти"),
                new("Медіа"),
                new("Збережені"),
            };


            ReplitedPosts = new ObservableCollection<Post>
            {
                new Post { AuthorProfileImage = "https://asianwiki.com/images/5/5e/Kim_So-Hyun-1999-p001.jpeg", AuthorName="twentyhils", PostDate=DateTime.Now, PostContent="Життя повне незабутніх пригод і сміху! Щойно мої друзі зібралися у нашій улюбленій кав'ярні, але замість кави отримали фітнес-напої! Вітаю, ми тепер суперспортивні! 😄", LikesCount=23, CommentsCount=1, ReplitsCount=2, SavesCount=2, SharesCount=1},
                new Post { AuthorProfileImage="https://staticg.sportskeeda.com/editor/2021/05/eb819-16209761972586-800.jpg", AuthorName = "justme.h", PostDate=DateTime.Now, PostContent="Полювання на квитки до концерту перетворилося на безумний комікс! Замість зручного онлайн-продажу, ми застряли в черзі з ексцентричними фанатами ", SharesCount=5, SavesCount=23, ReplitsCount=12, CommentsCount=6, LikesCount=117}
            };

            ChangeContentCommand = ReactiveCommand.Create<Segment>(ChangeContent);

            FollowCommand = ReactiveCommand.CreateFromTask<Guid>(FollowUser);

            OpenEditPageCommand = ReactiveCommand.CreateFromTask(OpenEditPage);

            GoBackCommand = ReactiveCommand.CreateFromTask(GoBack);

            OpenCreatePostPageCommand = ReactiveCommand.CreateFromTask(OpenCreatePostPage);

            CloseRecommendationsCommand = ReactiveCommand.Create(CloseRecommendations);

            LikePostCommand = ReactiveCommand.CreateFromTask<Post>(LikePost);

            CommentPostCommand = ReactiveCommand.CreateFromTask<Post>(CommentPost);

            SavePostCommand = ReactiveCommand.CreateFromTask<Post>(SavePost);

            OpenSettingsPageCommand = ReactiveCommand.CreateFromTask(OpenSettingsPage);

            Segments[0].IsSelected = true;

            IsRecommendationsVisible = true;
        }

        public async void LoadPosts()
        {
            Posts = new ObservableCollection<Post>();
            await new AsyncRequestBuilder(async () =>
            {
                var pagination = new PaginationIdRequest() { Id = _userStore.CurrentProfile.UserId};
                var posts = await _apiClient.BaseApi.PostClient.GetPostsAsync(pagination);
                if (posts == null) return;
                foreach (var post in posts.Items)
                {
                    Posts.Add(post.ToViewModel());
                }
            }).ExecuteAsync();
        }

        private async Task LikePost(Post post)
        {
            post.IsLiked = !post.IsLiked;
            post.LikesCount += (post.IsLiked ? 1 : -1);
            await new AsyncRequestBuilder(async () =>
            {
                if (post.IsLiked)
                    await _apiClient.BaseApi.PostClient.CreatePostLikeAsync(post.Id);
                else
                    await _apiClient.BaseApi.PostClient.DeletePostLikeAsync(post.Id);
            }).ExecuteAsync();
        }

        private async Task SavePost(Post post)
        {
            post.IsSaved = !post.IsSaved;
            post.SavesCount += (post.IsSaved ? 1 : -1);
            await new AsyncRequestBuilder(async () =>
            {
                if (post.IsSaved)
                    await _apiClient.BaseApi.PostClient.PinPostAsync(post.Id);
                else
                    await _apiClient.BaseApi.PostClient.UnpinPostAsync(post.Id);
            }).ExecuteAsync();
        }

        private async Task CommentPost(Post post)
        {
            await _navigationService.NavigateToAsync(nameof(SelectedPostPage));

            _messagingService.Send(this, "SelectedPost", post);
        }

        private void Subscribe()
        {
            _messagingService.Subscribe<FeedDashboardViewModel, Profile>(this, "UsersProfile", async (viewModel, profile) => await OnUsersProfileReceived(viewModel, profile));
        }

        private async Task OnUsersProfileReceived(FeedDashboardViewModel sender, Profile profile)
        {
            UserPosts = new ObservableCollection<Post>();
            UserProfile = profile;
            await new AsyncRequestBuilder(async () =>
            {
                var pagination = new PaginationIdRequest() { Id = UserProfile.UserId };
                var posts = await _apiClient.BaseApi.PostClient.GetPostsAsync(pagination);
                if (posts == null) return;
                foreach (var post in posts.Items)
                {
                    UserPosts.Add(post.ToViewModel());
                }
            }).ExecuteAsync();
        }

        private void OnProfileEdited(EditProfileViewModel sender, Profile profile) => Profile = profile;

        private void OnNewPostReceived(CreatePostViewModel sender, Post post)
        {
            Posts.Add(_postStore.CurrentPost);
            Posts = new ObservableCollection<Post>(Posts.OrderByDescending(p => p.PostDate));
        }

        private void CloseRecommendations() => IsRecommendationsVisible = false;

        private async Task OpenCreatePostPage() => await _navigationService.NavigateToAsync(nameof(CreatePostPage));

        private async Task GoBack() => await _navigationService.GoBackAsync();

        private async Task OpenEditPage() => await _navigationService.NavigateToAsync(nameof(EditProfilePage));

        private async Task OpenSettingsPage() => await _navigationService.NavigateToAsync(nameof(SettingsPage));

        private void ChangeContent(Segment segment) => Segments.ToList().ForEach(seg => seg.IsSelected = segment == seg);

        private async Task FollowUser(Guid userId)
        {
            UserProfile.IsSubscribed = !UserProfile.IsSubscribed;
            UserProfile.Followers += (UserProfile.IsSubscribed ? 1 : -1);
            Profile.Following += (UserProfile.IsSubscribed ? 1 : -1);

            await new AsyncRequestBuilder(async () =>
            {
                if (UserProfile.IsSubscribed)
                    await _apiClient.BaseApi.SubscriptionClient.SubscribeAsync(userId);
                else
                    await _apiClient.BaseApi.SubscriptionClient.UnsubscribeAsync(userId);
            }).ExecuteAsync();
        }
    }
}