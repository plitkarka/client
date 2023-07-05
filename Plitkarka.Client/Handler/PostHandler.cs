using Plitkarka.Client.Models;

namespace Plitkarka.Client.Handler;

public class PostHandler
{
    public static string CreatePost()
    {
        return "posts";
    }
    public static string DeletePost(Guid PostId)
    {
        return $"posts?PostId={PostId}";
    }

    public static string GetPosts(PaginationIdRequest request)
    {
        return request switch
        {
            var res when (res.Id == Guid.Empty && res.Page == 0) => "posts/all",
            var res when (res.Page == 0) => $"posts/all?Filter={request.Id}",
            var res when (res.Id == Guid.Empty) => $"posts/all?Page={request.Page}",
            _ => $"posts/all?Filter={request.Id}&Page={request.Page}"
        };
    }

    public static string GetMediaPosts(PaginationIdRequest request)
    {
        return request switch
        {
            var res when (res.Id == Guid.Empty && res.Page == 0) => "posts/all/media",
            var res when (res.Page == 0) => $"posts/all/media?Filter={request.Id}",
            var res when (res.Id == Guid.Empty) => $"posts/all/media?Page={request.Page}",
            _ => $"posts/all/media?Filter={request.Id}&Page={request.Page}"
        };
    }

    public static string TogglePostLike(Guid PostId)
    {
        return $"posts/like?PostId={PostId}";
    }

    public static string GetLikedPosts(PaginationIdRequest request)
    {
        return request switch
        {
            var res when (res.Id == Guid.Empty && res.Page == 0) => "posts/like/all",
            var res when (res.Page == 0) => $"posts/like/all?Filter={request.Id}",
            var res when (res.Id == Guid.Empty) => $"posts/like/all?Page={request.Page}",
            _ => $"posts/like/all?Filter={request.Id}&Page={request.Page}"
        };
    }

    public static string TogglePinPost(Guid PostId)
    {
        return $"posts/pin?PostId={PostId}";
    }

    public static string GetPinnedPosts(PaginationIdRequest request)
    {
        return request switch
        {
            var res when (res.Id == Guid.Empty && res.Page == 0) => "posts/pin/all",
            var res when (res.Page == 0) => $"posts/pin/all?Filter={request.Id}",
            var res when (res.Id == Guid.Empty) => $"posts/pin/all?Page={request.Page}",
            _ => $"posts/pin/all?Filter={request.Id}&Page={request.Page}"
        };
    }

    public static string ToggleSharePost(Guid PostId)
    {
        return $"posts/share?PostId={PostId}";
    }

    public static string GetSharedPosts(PaginationIdRequest request)
    {
        return request switch
        {
            var res when (res.Id == Guid.Empty && res.Page == 0) => "posts/share/all",
            var res when (res.Page == 0) => $"posts/share/all?Filter={request.Id}",
            var res when (res.Id == Guid.Empty) => $"posts/share/all?Page={request.Page}",
            _ => $"posts/share/all?Filter={request.Id}&Page={request.Page}"
        };
    }

    public static string GetFeed(PaginationRequest request)
    {
        return request switch
        {
            var res when (res.Page == 0) => $"posts/feed",
            _ => $"posts/feed?Page={request.Page}"
        };
    }

    public static string SearchPosts(PaginationTextRequest request)
    {
        return request switch
        {
            var res when (res.Page == 0) => $"posts/search?Filter={request.Filter}",
            _ => $"posts/search?Filter={request.Filter}&Page={request.Page}"
        };
    }
}
