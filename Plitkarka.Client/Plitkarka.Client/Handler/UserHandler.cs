using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Pagination;

namespace Plitkarka.Client.Handler;

public class UserHandler
{
    public static string GetUserById(Guid id)
    {
        return "user?userId=" + id.ToString();
    }
    public static string GetAllUsers(PaginationTextRequest body)
    {
        return body switch
        {
            var res when (res.Filter == string.Empty && res.Page == 0) => "user/all",
            var res when (res.Page == 0) => $"user/all?Filter={body.Filter}",
            var res when (res.Filter == string.Empty) => $"user/all?Page={body.Page}",
            _ => $"user/all?Filter={body.Filter}&Page={body.Page}"
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
}
