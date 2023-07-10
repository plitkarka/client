namespace Plitkarka.Client.Models.User;

public record UserData : User
{
    public string? Description { get; set; }

    public string? Link { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime LastLoginDate { get; set; }

    public int SubscribersCount { get; set; }

    public int SubscriptionsCount { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsAuthorized { get; set; }

    public bool IsSubscribed { get; set; }
}
