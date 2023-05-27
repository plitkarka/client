﻿using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Infrastructure.Services;
using Plitkarka.Models;
using Plitkarka.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Plitkarka.ViewModels;

public class FeedDashboardViewModel : ReactiveObject
{
    private readonly INavigationService _navigationService;
    private readonly IMessagingService _messagingService;

    [Reactive] public ObservableCollection<Post> Posts { get; set; }

    public ReactiveCommand<Post, Unit> OpenSelectedPostCommand { get; }

    public FeedDashboardViewModel(INavigationService navigationService, IMessagingService messagingService)
    {
        _navigationService = navigationService;
        _messagingService = messagingService;

        Posts = new ObservableCollection<Post>
        {
            new()
            {
                AuthorProfileImage = "https://example.com/user1.jpg", AuthorName = "firefly.li",
                PostDate = DateTime.Now,
                PostContent =
                    "Стрес на роботі може стати складним викликом для багатьох людей\n#стрес #робота #психологія",
                LikesCount = 648, CommentsCount = 34, ReplitsCount = 87, SavesCount = 93, SharesCount = 4
            },
            new()
            {
                AuthorProfileImage = "https://example.com/user2.jpg", AuthorName = "art_genius",
                PostDate = DateTime.Now,
                PostContent =
                    "Крута стаття \"5 Creative Color Combinations for Your Next Design Project\" - допоможе вибрати комбінації кольорів для дизайну\n#colordesign #designprinciples #negativespace",
                LikesCount = 10, CommentsCount = 5, ReplitsCount = 4, SavesCount = 2, SharesCount = 1
            },
            new()
            {
                AuthorProfileImage = "https://example.com/user3.jpg", AuthorName = "alla.kurilenko",
                PostDate = DateTime.Now,
                PostContent =
                    "Нова річ у моєму житті - відкрила для себе йогу! Це така потрібна практика для зняття стресу та підвищення концентрації. А ще знайшла чудову студію з чайним баром, де можна розслабитись після тренування. Рекомендую всім спробувати!\n#yoga #wellness",
                CommentsCount = 500, LikesCount = 20000, ReplitsCount = 450, SavesCount = 200, SharesCount = 100
            }
        };

        OpenSelectedPostCommand = ReactiveCommand.CreateFromTask<Post>(OpenSelectedPost);
    }

    private async Task OpenSelectedPost(Post post)
    {
        await _navigationService.NavigateToAsync(nameof(SelectedPostPage));

        await Application.Current.Dispatcher.DispatchAsync(() => _messagingService.Send(this, "SelectedPost", post));
    }

}