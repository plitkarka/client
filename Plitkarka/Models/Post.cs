using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Plitkarka.Models;

public class Post : ReactiveObject
{
    public string AuthorProfileImage { get; set; }
    public string AuthorName { get; set; }
    public DateTime PostDate { get; set; }
    public string PostContent { get; set; }
    public string PostImage { get; set; }
    [Reactive] public int LikesCount { get; set; }
    [Reactive] public int CommentsCount { get; set; }
    [Reactive] public int ReplitsCount { get; set; }
    [Reactive] public int SharesCount { get; set; }
    [Reactive] public int SavesCount { get; set; }
    [Reactive] public bool IsLiked { get; set; }
    [Reactive] public bool IsSaved { get; set; }
}