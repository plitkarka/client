using Plitkarka.Views;

namespace Plitkarka;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }
    
    private void RegisterRoutes()
    {
        Routing.RegisterRoute("profile", typeof(ProfilePage));
        Routing.RegisterRoute("login", typeof(LoginPage));
        Routing.RegisterRoute("registration", typeof(RegistrationPage));
        Routing.RegisterRoute("main", typeof(MainPage));
        Routing.RegisterRoute("editprofile", typeof(EditProfilePage));
        Routing.RegisterRoute("userprofile", typeof(UserProfilePage));
    }
}