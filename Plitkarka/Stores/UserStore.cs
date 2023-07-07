using Plitkarka.Models;

namespace Plitkarka.Stores;

public class UserStore
{
    public Profile CurrentProfile { get; set; }

    public UserStore()
    {
        CurrentProfile = new Profile
        {
            PhotoUrl = "https://pbs.twimg.com/media/FTOgOCgVEAAXvxL?format=jpg&name=large",
            Name = "Тарас Дерденчук",
            Nickname = "mrdurden",
            Bio = "І так народився він... Тарас Дерденчук",
            Link = "https://www.youtube.com/watch?v=J8FRBYOFu2w",
            Followers = 1245,
            Following = 432,
            UserId = 1
        };
    }
}