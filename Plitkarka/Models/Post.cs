namespace Plitkarka.Models;

public class Post
{
    public string AuthorProfileImage { get; set; }
    public string AuthorName { get; set; }
    public DateTime PostDate { get; set; }
    public string PostContent { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public int ReplitsCount { get; set; }
    public int SharesCount { get; set; }
    public int SavesCount { get; set; }
}