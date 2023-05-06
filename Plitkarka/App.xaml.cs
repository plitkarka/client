namespace Plitkarka;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        var app = MauiProgram.CreateMauiApp();
        MainPage = app.Services.GetService<AppShell>();
    }
}