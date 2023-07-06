using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Models;
using Plitkarka.Stores;
using Plitkarka.Views;

namespace Plitkarka.ViewModels
{
    public class ProfileViewModel : ReactiveObject
    {
        private readonly INavigationService _navigationService;
        private readonly IMessagingService _messagingService;
        private readonly UserStore _userStore;

        [Reactive] public bool IsRecommendationsVisible { get; set; }

        [Reactive] public Profile Profile { get; set; }

        [Reactive] public ObservableCollection<Segment> Segments { get; set; }

        [Reactive] public ObservableCollection<Profile> Recommendations { get; set; }

        [Reactive] public ObservableCollection<Post> Posts { get; set; }

        [Reactive] public ObservableCollection<Post> ReplitedPosts { get; set; }

        public ReactiveCommand<int, Unit> FollowCommand { get; }

        public ReactiveCommand<Segment, Unit> ChangeContentCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenEditPageCommand { get; }

        public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenCreatePostPageCommand { get; }

        public ReactiveCommand<Unit, Unit> CloseRecommendationsCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenSettingsPageCommand { get; }

        public ReactiveCommand<Post, Unit> LikePostCommand { get; }

        public ReactiveCommand<Post, Unit> CommentPostCommand { get; }

        public ReactiveCommand<Post, Unit> SavePostCommand { get; }

        public ProfileViewModel(INavigationService navigationService, IMessagingService messagingService, UserStore userStore)
        {
            _navigationService = navigationService;
            _messagingService = messagingService;
            _userStore = userStore;

            Subscribe();

            Profile = _userStore.CurrentProfile;
            
            Recommendations = new ObservableCollection<Profile>
            {
                new Profile { UserId = 1, Nickname = "vasilii", PhotoUrl = "https://st2.depositphotos.com/1037987/9820/i/950/depositphotos_98204712-stock-photo-man-having-breakfast-in-a.jpg" },
                new Profile { UserId = 2, Nickname = "aminamina", PhotoUrl = "https://img.freepik.com/premium-photo/attractive-young-woman-sitting-alone-near-big-window-coffee-shop-table-with-cup-cappuccino-cake-relaxing-restaurant-during-free-time-young-female-having-rest-cafe-lifestyle-concept_365776-12533.jpg?w=2000" },
                new Profile { UserId = 3, Nickname = "katya.l", PhotoUrl = "https://d2gg9evh47fn9z.cloudfront.net/800px_COLOURBOX11110978.jpg" },
            };

            Segments = new ObservableCollection<Segment>
            {
                new("Плітки"),
                new("Репліти"),
                new("Медіа"),
                new("Збережені"),
            };

            // Posts = new ObservableCollection<Post>();
            Posts = new ObservableCollection<Post>
            {
                new Post{ AuthorProfileImage = Profile.PhotoUrl, AuthorName = Profile.Nickname, PostDate = DateTime.Now, PostContent = "Ітак, Україна. Хто б міг подумати? Якби ви хотіли відчути справжній колорит країни, вам було б потрібно пройтися головною вулицею з почуттям голоду в шлунку та окою, сповненими незабутніх спогадів.", LikesCount = 230, CommentsCount = 15 , ReplitsCount = 22, SavesCount = 35, SharesCount = 23},
                new Post{ AuthorProfileImage = Profile.PhotoUrl, AuthorName = Profile.Nickname, PostDate = DateTime.Now, PostContent = "Мій перший досвід з салом в Україні був чимось на зразок релігійного прориву. Це вже не просто їжа, це символ, традиція, щось таке, що об'єднує цю країну разом, як крейдові лінії на боксерському рингу.", LikesCount = 410, CommentsCount = 35 , ReplitsCount = 24, SavesCount = 12, SharesCount = 31},
                new Post{ AuthorProfileImage = Profile.PhotoUrl, AuthorName = Profile.Nickname, PostDate = DateTime.Now, PostContent = "Отже, Україна. Місце, де сало і красиві жінки не просто частина культури, але і символи країни, яка бореться, любить і живе з повною інтенсивністю, що вирізняє її з-поміж інших. Незабутній досвід, який лишає відбиток у вашому серці. Це - Тарас Дерденчук, і я був тут.",PostImage = "https://cdn.gencraft.com/prod/user/c7fd8431-e18b-47ce-a7a5-3875ed17e82d/def562aa-3ff0-4ecc-b52b-615f023e3163/images/image0_1024_1024_watermark.jpg?Expires=1688631281&Signature=aRNn47-uqPkNUTDGUCcLt9wyTWYDF7J1LgzWKp39xSl~yY1x8WthhrKh5~f6kPtNIkMbJ4XG53PRCO6fgbg~CstCrthrgOB~zVk65Z0tqxTNYkd2xRIQxUDDpZjXktPPqrmD0oK8aqeFDAQZhuhi3pZj13fAn7yDuMO~O0kSGG2F9gr39tCGVvbxY5N8~u~LtjAyRc2Ch1cKms2LSKsdHMVk4Fmv9viuVmyIFNSrDYSbo9QXOvC5~aW-PyyY0Db4oLLxm2wCeFmreH8g6JZVoQC2K8qQ3AypB1pbKUkT6InZcRy66ceI9BHUpktZ6nWKMJ4HdOPkGC2RcSk0gzt6nw__&Key-Pair-Id=K3RDDB1TZ8BHT8", CommentsCount = 500, LikesCount = 20000, ReplitsCount = 450, SavesCount = 200, SharesCount = 100}
            };

            //Posts = new ObservableCollection<Post>()
            //{
            //    new Post{ AuthorProfileImage = "https://img.freepik.com/free-photo/happy-man-restaurant-drinking-coffee_23-2148395383.jpg?w=2000", AuthorName="oleksandrpetrov", PostContent = "Кава чи не кава? Ось в чому питання...", PostDate = DateTime.Now, LikesCount=20, CommentsCount= 3, SavesCount=2, SharesCount=4, ReplitsCount=2}
            //};

            ReplitedPosts = new ObservableCollection<Post>
            {
                new Post {AuthorProfileImage = "https://asianwiki.com/images/5/5e/Kim_So-Hyun-1999-p001.jpeg", AuthorName="twentyhils", PostDate=DateTime.Now, PostContent="Життя повне незабутніх пригод і сміху! Щойно мої друзі зібралися у нашій улюбленій кав'ярні, але замість кави отримали фітнес-напої! Вітаю, ми тепер суперспортивні! 😄", LikesCount=23, CommentsCount=1, ReplitsCount=2, SavesCount=2, SharesCount=1},
                new Post{AuthorProfileImage="https://staticg.sportskeeda.com/editor/2021/05/eb819-16209761972586-800.jpg", AuthorName = "justme.h", PostDate=DateTime.Now, PostContent="Полювання на квитки до концерту перетворилося на безумний комікс! Замість зручного онлайн-продажу, ми застряли в черзі з ексцентричними фанатами ", SharesCount=5, SavesCount=23, ReplitsCount=12, CommentsCount=6, LikesCount=117}
            };

            ChangeContentCommand = ReactiveCommand.Create<Segment>(ChangeContent);

            FollowCommand = ReactiveCommand.CreateFromTask<int>(FollowUser);

            OpenEditPageCommand = ReactiveCommand.CreateFromTask(OpenEditPage);

            GoBackCommand = ReactiveCommand.CreateFromTask(GoBack);

            OpenCreatePostPageCommand = ReactiveCommand.CreateFromTask(OpenCreatePostPage);

            CloseRecommendationsCommand = ReactiveCommand.Create(CloseRecommendations);

            LikePostCommand = ReactiveCommand.Create<Post>(LikePost);

            CommentPostCommand = ReactiveCommand.CreateFromTask<Post>(CommentPost);

            SavePostCommand = ReactiveCommand.Create<Post>(SavePost);

            OpenSettingsPageCommand = ReactiveCommand.CreateFromTask(OpenSettingsPage);

            Segments[0].IsSelected = true;

            IsRecommendationsVisible = true;
        }

        private void LikePost(Post post)
        {
            post.IsLiked = !post.IsLiked;
            post.LikesCount += (post.IsLiked ? 1 : -1);
        }

        private void SavePost(Post post)
        {
            post.IsSaved = !post.IsSaved;
            post.SavesCount += (post.IsSaved ? 1 : -1);
        }

        private async Task CommentPost(Post post)
        {
            await _navigationService.NavigateToAsync(nameof(SelectedPostPage));

            _messagingService.Send(this, "SelectedPost", post);
        }

        private void Subscribe()
        {
            _messagingService.Subscribe<CreatePostViewModel, Post>(this, "NewPost", OnNewPostReceived);
            _messagingService.Subscribe<EditProfileViewModel, Profile>(this, "EditedProfile", OnProfileEdited);
        }

        private void OnProfileEdited(EditProfileViewModel sender, Profile profile) => Profile = profile;

        private void OnNewPostReceived(CreatePostViewModel sender, Post post)
        {
            Posts.Add(post);
            Posts = new ObservableCollection<Post>(Posts.OrderByDescending(p => p.PostDate));
        }

        private void CloseRecommendations() => IsRecommendationsVisible = false;

        private async Task OpenCreatePostPage() => await _navigationService.NavigateToAsync(nameof(CreatePostPage));

        private async Task GoBack() => await _navigationService.GoBackAsync();

        private async Task OpenEditPage()
        {
            await _navigationService.NavigateToAsync(nameof(EditProfilePage));

            _messagingService.Send(this, "ProfileToChange", Profile);
        }

        private async Task OpenSettingsPage()
        {
            await _navigationService.NavigateToAsync(nameof(SettingsPage));
        }

        private void ChangeContent(Segment segment)
        {
            foreach (var seg in Segments)
            {
                seg.IsSelected = (seg == segment);
            }
        }

        private async Task FollowUser(int userId)
        {
            await Task.Delay(1000);
        }
    }
}