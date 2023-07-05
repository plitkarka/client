namespace Plitkarka;

public partial class App : Application
{
    private MauiApp _app;

    public App()
    {
        InitializeComponent();        
        _app = MauiProgram.CreateMauiApp();
        MainPage = _app.Services.GetService<AppShell>();
    }

    protected override void OnStart()
    {
        var mainPage = _app.Services.GetRequiredService<MainPage>();
        MainPage.Navigation.PushAsync(mainPage);
    }
}