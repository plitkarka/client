using Newtonsoft.Json;
using System.Text;

namespace Plitkarka.Client.Services;

public class JsonServices
{
    public static async Task SerializeToFileAsync(string fileName, string json)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            byte[] serializedResult = Encoding.UTF8.GetBytes(json);
            await fs.WriteAsync(serializedResult);
            fs.Close();
        }
    }

    public static async Task<T> DeserializeFromFileAsync<T>(string fileName)
    {
        string json;

        using (StreamReader r = new StreamReader(fileName))
        {
            json = await r.ReadToEndAsync();
            r.Close();
        }

        return JsonConvert.DeserializeObject<T>(json);
    }

    public static async Task ClearFileAsync(string fileName)
    {
        await File.WriteAllTextAsync(fileName, string.Empty);
    }
}
