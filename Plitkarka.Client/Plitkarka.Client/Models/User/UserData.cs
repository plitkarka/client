namespace Plitkarka.Client.Models.User;

public record UserData : User
{
    public DateTime BirthDate { get; set; }
    public DateTime LastLoginDate { get; set; }
    public int SubscribersCount { get; set; }
    public int SubscriptionsCount { get; set; }
}
