using Newtonsoft.Json;
using Plitkarka.Client.Handler;
using Plitkarka.Client.Models.TokenPair;
using System.Text;

namespace Plitkarka.Client.Services;

public class DeviceIdService
{
    public static async Task<string> GetDeviceRegistrationId()
    {
        if (File.Exists(FileHandler.GetDeviceIdFileLocation()) && new FileInfo(FileHandler.GetDeviceIdFileLocation()).Length > 0)
        {
            var res = await JsonServices.DeserializeFromFileAsync<UniqueIdentifierRequest>(FileHandler.GetDeviceIdFileLocation());
            return res.UniqueIdentifier;
        }
        else
        {
            Guid deviceId = Guid.NewGuid();

            using (FileStream fs = new FileStream(FileHandler.GetDeviceIdFileLocation(), FileMode.OpenOrCreate))
            {
                UniqueIdentifierRequest request = new UniqueIdentifierRequest() { UniqueIdentifier = deviceId.ToString() };
                byte[] serializedResult = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request, Formatting.Indented));
                await fs.WriteAsync(serializedResult);
            }

            return deviceId.ToString();
        }
    }
}
