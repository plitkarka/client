using Plitkarka.Client.Models;

namespace Plitkarka.Client.Handler;

public class UserHandler
{
    public static string GetUserById(Guid id)
    {
        return "user?userId=" + id.ToString();
    }

    public static string GetAllUsers(PaginationTextRequest request)
    {
        return request switch
        {
            var res when (request == null || res.Filter == string.Empty && res.Page == 0) => "user/all",
            var res when (res.Page == 0) => $"user/all?Filter={request.Filter}",
            var res when (res.Filter == string.Empty) => $"user/all?Page={request.Page}",
            _ => $"user/all?Filter={request.Filter}&Page={request.Page}"
        };
    }

    public static string GetUserImageById(Guid id)
    {
        return "user/image?userId=" + id.ToString();
    }

    public static string SendUserImage()
    {
        return "user/image";
    }

    public static string UpdateUserProfile()
    {
        return "user";
    }
}
