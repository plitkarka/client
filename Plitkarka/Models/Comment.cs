using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Plitkarka.Models
{
	public class Comment: ReactiveObject
	{
        public string AuthorProfileImage { get; set; }
        public string AuthorName { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }
    }
}

