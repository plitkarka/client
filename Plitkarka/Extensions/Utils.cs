using System;
using Plitkarka.Client.Models.Comments;
using Plitkarka.Client.Models.Post;
using Plitkarka.Client.Models.User;
using Plitkarka.Models;

namespace Plitkarka.Extensions;

public static class Utils
{
    public static Post ToViewModel(this PostResponse response)
    {
        var post = new Post();
        post.Id = response.Id;
        post.UserId = response.UserPreview.Id;
        post.AuthorProfileImage = response.UserPreview.ImageUrl;
        post.AuthorName = response.UserPreview.Name;
        post.PostDate = response.CreatedDate;
        post.PostContent = response.TextContent;
        post.PostImage = response.ImageUrl;
        post.LikesCount = response.LikesCount;
        post.CommentsCount = response.CommentsCount;
        post.ReplitsCount = response.SharesCount;
        post.SavesCount = response.PinsCount;
        post.IsLiked = response.IsLiked;
        post.IsSaved = response.IsPinned;
        post.SharesCount = 0;

        return post;
    }

    public static Comment ToViewModel(this CommentResponse response)
    {
        var comment = new Comment();
        comment.AuthorProfileImage = response.UserPreview.ImageUrl;
        comment.AuthorName = response.UserPreview.Name;
        comment.CommentDate = response.CreateDate;
        comment.CommentContent = response.TextContent;
        comment.LikesCount = response.LikesCount;
        comment.IsLiked = response.IsLiked;

        return comment;
    }

    public static Profile ToViewModel(this UserData response)
    {
        var profile = new Profile();
        profile.UserId = response.Id;
        profile.Nickname = response.Login;
        profile.Name = response.Name;
        profile.Followers = response.SubscribersCount;
        profile.Following = response.SubscriptionsCount;
        profile.Bio = response.Description;
        profile.Link = response.Link;
        profile.PhotoUrl = response.ImageUrl;

        return profile;
    }

    public static Profile ToViewModel(this UserPreview user)
    {
        var profile = new Profile();
        profile.PhotoUrl = user.ImageUrl;
        profile.Nickname = user.Login;
        profile.Name = user.Name;

        return profile;
    }


    public static PostResponse ToServerModel(this Post post)
    {
        var response = new PostResponse();
        response.UserPreview = new UserPreview
        {
            ImageUrl = post.AuthorProfileImage,
            Name = post.AuthorName,
            Id = post.UserId
        };
        response.Id = post.Id;
        response.CreatedDate = post.PostDate;
        response.TextContent = post.PostContent;
        response.ImageUrl = post.PostImage;
        response.LikesCount = post.LikesCount;
        response.CommentsCount = post.CommentsCount;
        response.SharesCount = post.ReplitsCount;
        response.PinsCount = post.SavesCount;
        response.IsLiked = post.IsLiked;
        response.IsPinned = post.IsSaved;

        return response;
    }


    public static CommentResponse ToServerModel(this Comment comment)
    {
        var response = new CommentResponse();
        response.UserPreview = new UserPreview
        {
            ImageUrl = comment.AuthorProfileImage,
            Name = comment.AuthorName
        };
        response.CreateDate = comment.CommentDate;
        response.TextContent = comment.CommentContent;
        response.LikesCount = comment.LikesCount;
        response.IsLiked = comment.IsLiked;

        return response;
    }

    public static UserData ToServerModel(this Profile profile)
    {
        var response = new UserData();
        response.Id = profile.UserId;
        response.Login = profile.Nickname;
        response.Name = profile.Name;
        response.SubscribersCount = profile.Followers;
        response.SubscriptionsCount = profile.Following;
        response.Description = profile.Bio;
        response.Link = profile.Link;
        response.ImageUrl = profile.PhotoUrl;

        return response;
    }

}
