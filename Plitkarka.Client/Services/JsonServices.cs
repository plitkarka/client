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

    public static async Task<T> DeserializeFromFileAsync<T>(string filePath) where T : class
    {
        if (!File.Exists(filePath))
        {
            // File does not exist, handle appropriately.
            // Maybe return null, throw a specific exception, or create the file.
            return null;
        }

        using (StreamReader file = new StreamReader(filePath))
        {
            string content = await file.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }

    public static async Task ClearFileAsync(string fileName)
    {
        await File.WriteAllTextAsync(fileName, string.Empty);
    }
}
