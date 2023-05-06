using CommunityToolkit.Maui;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Infrastructure.Services;
using Plitkarka.ViewModels;
using Plitkarka.Views;

namespace Plitkarka;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        ConfigureServices(builder);
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }

    private static void ConfigureServices(MauiAppBuilder builder)
    {
        builder.Services.AddTransient<INavigationService, NavigationService>();

        builder.Services.AddSingleton<ProfileViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<RegistrationViewModel>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<EditProfileViewModel>();

        builder.Services.AddTransient(s => new ProfilePage { BindingContext = s.GetRequiredService<ProfileViewModel>() });
        builder.Services.AddTransient(s => new LoginPage { BindingContext = s.GetService<LoginViewModel>() });
        builder.Services.AddTransient(s => new RegistrationPage { BindingContext = s.GetService<RegistrationViewModel>() });
        builder.Services.AddTransient(s => new MainPage { BindingContext = s.GetService<MainViewModel>() });
        builder.Services.AddTransient(s => new EditProfilePage { BindingContext = s.GetService<EditProfileViewModel>() });
        builder.Services.AddTransient(s => new UserProfilePage { BindingContext = s.GetService<ProfileViewModel>() });
        builder.Services.AddTransient<AppShell>();
        
    }
}