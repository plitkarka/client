namespace Plitkarka.Client.Models;

public class UserData
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime LastLoginDate { get; set; }
    public int SubscribersCount { get; set; }
    public int SubscriptionsCount { get; set; }
}
