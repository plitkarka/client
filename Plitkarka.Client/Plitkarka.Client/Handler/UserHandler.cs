namespace Plitkarka.Client.Handler;

public class UserHandler
{
    public static string GetUserById(Guid id)
    {
        return "user?userId=" + id.ToString();
    }
    public static string GetAllUsers(string filter="0",int page=0)
    {
        return $"user/all?Filter={filter}&Page={page}";
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
