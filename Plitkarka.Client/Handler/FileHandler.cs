using System.Reflection;

namespace Plitkarka.Client.Handler;

public class FileHandler
{
    private static readonly string FILE_FOLDER = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + @"\Plitkarka.Client";

    public static string GetTokenPairFileLocation() => FILE_FOLDER + @"\Files\tokenpair.json";

    public static Stream GetHttpConfigFileLocation() => Assembly.GetExecutingAssembly().GetManifestResourceStream("Plitkarka.Client.httpclientconfig.json");

    public static string GetDeviceIdFileLocation() => FILE_FOLDER + @"\Files\deviceid.json";
}
