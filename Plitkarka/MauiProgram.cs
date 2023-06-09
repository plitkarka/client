﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Handlers;
using Plitkarka.Client.Handler;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Repositories;
using Plitkarka.Client.Services;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Infrastructure.Services;
using Plitkarka.Stores;
using Plitkarka.ViewModels;
using Plitkarka.Views;
using Plitkarka.Views.ContentViews;
using System.Net.Http;

namespace Plitkarka;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        ConfigureViewsAndViewModels(builder);
        ConfigureServices(builder);
        ConfigureStores(builder);
        ConfigureApiClient(builder);
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        ConfigureHandlers();

        var stream = FileHandler.GetHttpConfigFileLocation();
        var httpConfig = new ConfigurationBuilder()
         .AddJsonStream(stream)
         .Build();

        builder.Services.AddSingleton(sp => new HttpClient
        {
            BaseAddress = new Uri(httpConfig.GetSection("BaseUrl").Value)
        });

        return builder.Build();
    }

    private static void ConfigureServices(MauiAppBuilder builder)
    {
        builder.Services.AddTransient<INavigationService, NavigationService>();
        builder.Services.AddTransient<IMessagingService, MessagingService>();
    }

    private static void ConfigureApiClient(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IHttpClient, MyHttpClient>();
        builder.Services.AddSingleton<IUserClient, UserClient>();
        builder.Services.AddSingleton<IAuthClient, AuthClient>();
        builder.Services.AddSingleton<IPostClient, PostClient>();
        builder.Services.AddSingleton<ICommentClient, CommentClient>();
        builder.Services.AddSingleton<ISubscriptionClient, SubscriptionClient>();
        builder.Services.AddSingleton<IResetPasswordClient, ResetPasswordClient>();
        builder.Services.AddSingleton<IBaseNetworkApi, BaseNetworkApi>();
        builder.Services.AddSingleton<IApiClient, ApiClient>();
    }

    private static void ConfigureStores(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<UserStore>();
        builder.Services.AddSingleton<PostStore>();
    }

    private static void ConfigureViewsAndViewModels(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<ProfileViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<RegistrationViewModel>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<EditProfileViewModel>();
        builder.Services.AddSingleton<FeedDashboardViewModel>();
        builder.Services.AddSingleton<SelectedPostViewModel>();
        builder.Services.AddSingleton<SearchViewModel>();
        builder.Services.AddSingleton<CreatePostViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<ChangePasswordViewModel>();
        builder.Services.AddSingleton<BlockedUsersViewModel>();

        builder.Services.AddTransient<ProfilePage>();
        //builder.Services.AddTransient(s => new ProfilePage { BindingContext = s.GetRequiredService<ProfileViewModel>() });
        builder.Services.AddTransient(s => new LoginPage { BindingContext = s.GetService<LoginViewModel>() });
        builder.Services.AddTransient(s => new RegistrationPage { BindingContext = s.GetService<RegistrationViewModel>() });
        builder.Services.AddTransient(s => new MainPage { BindingContext = s.GetService<MainViewModel>() });
        builder.Services.AddTransient(s => new EditProfilePage { BindingContext = s.GetService<EditProfileViewModel>() });
        builder.Services.AddTransient(s => new UserProfilePage { BindingContext = s.GetService<ProfileViewModel>() });
        builder.Services.AddTransient(s => new FeedDashboard { BindingContext = s.GetService<FeedDashboardViewModel>() });
        builder.Services.AddTransient(s => new SelectedPostPage { BindingContext = s.GetService<SelectedPostViewModel>() });
        builder.Services.AddTransient(s => new SearchPage { BindingContext = s.GetService<SearchViewModel>() });
        builder.Services.AddTransient(s => new CreatePostPage { BindingContext = s.GetService<CreatePostViewModel>() });
        builder.Services.AddTransient(s => new SettingsPage { BindingContext = s.GetService<SettingsViewModel>() });
        builder.Services.AddTransient(s => new ChangePasswordPage { BindingContext = s.GetService<ChangePasswordViewModel>() });
        builder.Services.AddTransient(s => new BlockedUsersPage { BindingContext = s.GetService<BlockedUsersViewModel>() });
        builder.Services.AddTransient(s => new ChatPage());

        builder.Services.AddTransient<PlitkiView>();
        builder.Services.AddTransient<ReplitsView>();
        builder.Services.AddTransient<MediaView>();
        builder.Services.AddTransient<VpodobaikiView>();

        builder.Services.AddSingleton<AppShell>();
    }

    private static void ConfigureHandlers()
    {
#if IOS
    SearchBarHandler.Mapper.AppendToMapping("CancelButtonColor", (handler, view) =>
    {
        handler.PlatformView.SetShowsCancelButton(false, false);
    });
#endif
    }
}