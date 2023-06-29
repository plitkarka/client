namespace Plitkarka.Client.Handler;

public class FileHandler
{
    private const string FILE_FOLDER = @"Files\";

    public static string GetTokenPairFileLocation() => FILE_FOLDER + "tokenpair.json";

    public static string GetHttpConfigFileLocation() => "httpclientconfig.json";

    public static string GetDeviceIdFileLocation() => FILE_FOLDER + "diviceid.json";
}
