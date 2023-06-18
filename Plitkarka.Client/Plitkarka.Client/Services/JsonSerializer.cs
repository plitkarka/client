using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace Plitkarka.Client.Services;

public class JsonSerializer
{
    public static async void SerializeToFile(string fileName, string json)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            byte[] serializedResult = Encoding.UTF8.GetBytes(json);
            await fs.WriteAsync(serializedResult);
        }
    }
    public static async void ClearFile(string fileName)
    {
        File.WriteAllTextAsync(fileName, string.Empty);
    }
}
