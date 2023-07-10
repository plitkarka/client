using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Plitkarka.Client.Interfaces;
using Plitkarka.Infrastructure.Helpers;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Infrastructure.Services;
using Plitkarka.Models;
using Plitkarka.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Plitkarka.Extensions;

namespace Plitkarka.ViewModels;

public class FeedDashboardViewModel : ReactiveObject
{
    private readonly INavigationService _navigationService;
    private readonly IMessagingService _messagingService;
    private readonly IApiClient _apiClient;
    
    [Reactive] public ObservableCollection<Post> Posts { get; set; }

    public ReactiveCommand<Post, Unit> OpenSelectedPostCommand { get; }
    
    public ReactiveCommand<Post, Unit> LikePostCommand { get; }

    public ReactiveCommand<Post, Unit> OpenUsersProfileCommand { get; }

    public FeedDashboardViewModel(INavigationService navigationService, IMessagingService messagingService, IApiClient apiClient)
    {
        _navigationService = navigationService;
        _messagingService = messagingService;
        _apiClient = apiClient;

        Posts = new ObservableCollection<Post>();
        Init();

        OpenSelectedPostCommand = ReactiveCommand.CreateFromTask<Post>(OpenSelectedPost);
        
        LikePostCommand = ReactiveCommand.CreateFromTask<Post>(LikePost);

        OpenUsersProfileCommand = ReactiveCommand.CreateFromTask<Post>(OpenUsersProfile);
    }

    private async void Init()
    {
        await new AsyncRequestBuilder(async () =>
        {
            var feed = await _apiClient.BaseApi.PostClient.GetFeedAsync();
            if (feed == null) return;
            foreach (var post in feed.Items)
            {
                Posts.Add(post.ToViewModel());
            }
        }).ExecuteAsync();
    }

    private async Task OpenUsersProfile(Post post)
    {
        await new AsyncRequestBuilder(async () =>
        {
            var user = await _apiClient.BaseApi.UserClient.GetByIdAsync(post.UserId);
            _messagingService.Send(this, "UsersProfile", user.ToViewModel());
            await _navigationService.NavigateToAsync(nameof(UserProfilePage));
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

    private async Task OpenSelectedPost(Post post)
    {
        await _navigationService.NavigateToAsync(nameof(SelectedPostPage));

        await Application.Current.Dispatcher.DispatchAsync(() => _messagingService.Send(this, "SelectedPost", post));
    }
}