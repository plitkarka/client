using System;
using System.Reactive;
using System.Threading.Tasks;
using Plitkarka.Infrastructure.Helpers;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Infrastructure.Services;
using Plitkarka.Models;
using Plitkarka.Stores;
using Plitkarka.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;

namespace Plitkarka.ViewModels
{
    public class CreatePostViewModel : ReactiveValidationObject
    {
        private readonly INavigationService _navigationService;
        private readonly IMessagingService _messagingService;
        private readonly UserStore _userStore;

        [Reactive] public string PostImage { get; set; }

        [Reactive] public Profile Profile { get; set; }

        [Reactive] public string Content { get; set; }

        [Reactive] public int RemainingCharacters { get; set; }

        public ReactiveCommand<Unit, Unit> AddNewPostCommand { get; set; }

        public ReactiveCommand<Unit, Unit> AddImageCommand { get; set; }

        public CreatePostViewModel(INavigationService navigationService, IMessagingService messagingService, UserStore userStore)
        {
            _navigationService = navigationService;
            _messagingService = messagingService;
            _userStore = userStore;

            Profile = _userStore.CurrentProfile;

            this.WhenAnyValue(vm => vm.Content)
                   .Subscribe(content =>
                   {
                       RemainingCharacters = 350 - (content?.Length ?? 0);
                   });


            AddNewPostCommand = ReactiveCommand.CreateFromTask(AddNewPost,
            this.WhenAnyValue(vm => vm.Content,
                              vm => vm.PostImage,
                              vm => vm.RemainingCharacters,
                              (content, image, remaining) =>
                              (!string.IsNullOrWhiteSpace(content) &&
                              content.Length <= 350) ||
                              !string.IsNullOrWhiteSpace(image)));

            AddImageCommand = ReactiveCommand.CreateFromTask(AddImage);
        }

        private async Task AddImage()
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

                    PostImage = newFile;
                }
            })
            .HandleException<OperationCanceledException>(e => Console.WriteLine(e.Message))
            .ExecuteAsync();
        }

        private async Task AddNewPost()
        {
            var newPost = new Post();
            newPost.AuthorName = Profile.Nickname;
            newPost.AuthorProfileImage = Profile.PhotoUrl;
            newPost.PostDate = DateTime.Now;
            newPost.PostContent = Content;
            newPost.CommentsCount = 0;
            newPost.LikesCount = 0;
            newPost.ReplitsCount = 0;
            newPost.SavesCount = 0;
            newPost.SharesCount = 0;
            newPost.PostImage = PostImage;

            Content = PostImage = string.Empty;

            _messagingService.Send(this, "NewPost", newPost);

            await _navigationService.NavigateToAsync(nameof(FeedDashboard));
        }
    }
}
