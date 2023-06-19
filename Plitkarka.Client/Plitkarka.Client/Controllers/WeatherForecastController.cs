using Microsoft.AspNetCore.Mvc;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Authorization;
using Plitkarka.Client.Models.Comments;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.Post;
using Plitkarka.Client.Models.ResetPassword;
using Plitkarka.Client.Models.User;
using Plitkarka.Client.Repositories;
using Plitkarka.Client.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace Plitkarka.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private IApiClient _apiClient { get; init; }
        private MyHttpClient MyHttpClient { get; init; }

        public Controller(
            IApiClient apiClient,HttpClient httpClient)
        {
            _apiClient = apiClient;
            MyHttpClient = new MyHttpClient(httpClient);

            MyHttpClient = new AuthClient(httpClient);
        }

        [HttpPost("user/image")]
        public async Task<ActionResult<IdResponse>> SendUserImage([FromForm] SetUserImageRequestModel body)
        {
            var response = await _apiClient.BaseApi.UserClient.SetUserImageAsync(body);

            return Ok(response);
        }

        [HttpGet("user/image")]
        public async Task<ActionResult<StringResponse>> GetUserImageURL(
        [FromQuery] Guid userId)
        {
            var response = await _apiClient.BaseApi.UserClient.GetImageUrlByUserIdAsync(userId);

            return Ok(response);
        }
       
        [HttpGet("user")]
        public async Task<ActionResult<UserData>> GetUserData(
        [FromQuery] Guid userId)
        {
            var response = await _apiClient.BaseApi.UserClient.GetByIdAsync(userId);

            return Ok(response);
        }

        [HttpGet("user/all")]
        public async Task<ActionResult<PaginationResponse<UserPreview>>> GetAllUserData(
            [FromQuery] PaginationTextRequest body)
        {
            var response = await _apiClient.BaseApi.UserClient.GetAllAsync(body);

            return Ok(response);
        }

        //Auth

        [HttpPost("auth")]
        public async Task<ActionResult<StringResponse>> SingUp(
            [FromBody] SignUpRequest body)
        {
            var response = await _apiClient.BaseApi.AuthClient.SignUpAsync(body);

            return Ok(response);
        }

        [HttpPost("auth/signin")]
        public async Task<ActionResult<TokenPairResponse>> SingIn(
          [FromBody] SignInRequest body)
        {
            var response = await _apiClient.BaseApi.AuthClient.SignInAsync(body);

            return Ok(response);
        }

        [HttpGet("auth/signout")]
        public Task SingOut()
        {
            _apiClient.BaseApi.AuthClient.SignOut();

            return null;
        }

        [HttpPost("email")]
        public async Task<ActionResult<TokenPairResponse>> VerifyEmail(
          [FromBody] VerifyEmailRequest body)
        {
            var response = await _apiClient.BaseApi.AuthClient.VerifyEmailAsync(body);

            return Ok(response);
        }

        [HttpPut("email")]
        public async Task<ActionResult<StringResponse>> ResendVerificationCode(
         [FromBody] ResendVerificationCodeRequest body)
        {
            var response = await _apiClient.BaseApi.AuthClient.ResendVerificationCodeAsync(body);

            return Ok(response);
        }

        [HttpGet("refresh")]
        public async Task<ActionResult<TokenPairResponse>> RefreshTokenPair(
         [FromQuery] string refreshToken)
        {
            var response = await _apiClient.BaseApi.AuthClient.GetNewTokenPairAsync(refreshToken);

            return Ok(response);
        }

        //Reset

        [HttpPost("password/reset")]
        public async Task<ActionResult<StringResponse>> SendEmail(
         [FromBody] SendEmailRequest email)
        {
            var response = await _apiClient.BaseApi.ResetPasswordClient.SendEmailAsync(email);

            return Ok(response);
        }

        [HttpGet("password/reset")]
        public async Task<ActionResult<VerifyCodeRequest>> VerifyCode(
         [FromQuery] VerifyCodeRequest body)
        {
            var response = await _apiClient.BaseApi.ResetPasswordClient.VerifyCodeAsync(body);

            return Ok(response);
        }

        [HttpPut("password/reset")]
        public async Task<ActionResult<TokenPairResponse>> ResetPassword(
         [FromBody] ResetPasswordRequest body)
        {
            var response = await _apiClient.BaseApi.ResetPasswordClient.ResetPasswordAsync(body);

            return Ok(response);
        }

        //Sub

        [HttpPost("sub")]
        public async Task<ActionResult<IdResponse>> Subscribe(
         [FromQuery] Guid SubscribeToId)
        {
            var response = await _apiClient.BaseApi.SubscriptionClient.SubscribeAsync(SubscribeToId);

            return Ok(response);
        }

        [HttpDelete("sub")]
        public async Task<ActionResult<IdResponse>> Unsubscribe(
         [FromQuery] Guid UnsubscribeFromId)
        {
            await _apiClient.BaseApi.SubscriptionClient.UnsubscribeAsync(UnsubscribeFromId);

            return Ok();
        }

        [HttpGet("sub/subscribers/all")]
        public async Task<ActionResult<TokenPairResponse>> GetSubscribers(
             [FromQuery] Guid filter, int page)
        {
            var response = await _apiClient.BaseApi.SubscriptionClient.GetAllSubscribersAsync(new PaginationIdRequest() 
            { Id = filter, Page = page});

            return Ok(response);
        }

        [HttpGet("sub/subscription/all")]
        public async Task<ActionResult<TokenPairResponse>> GetSubscription(
             [FromQuery] Guid filter, int page)
        {
            var response = await _apiClient.BaseApi.SubscriptionClient.GetAllSuscriptionsAsync(new PaginationIdRequest()
            { Id = filter, Page = page });

            return Ok(response);
        }
    }
    [ApiController]
    [Route("[controller]")]
    public class SubComPostController : ControllerBase
    {
        private IApiClient _apiClient { get; init; }

        public SubComPostController(
            IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        [HttpPost("comment")]
        public async Task<ActionResult<IdResponse>> CreateComment
            ([FromBody] CreateCommentRequest request)
        {
            var response = await _apiClient.BaseApi.CommentClient.CreateCommentAsync(request);

            return Ok(response);
        }
        [HttpDelete("comment")]
        public async Task<ActionResult> DeleteComment
           ([FromQuery] Guid guid)
        {
            await _apiClient.BaseApi.CommentClient.DeleteCommentAsync(guid);

            return Ok();
        }
        [HttpGet("comment/all")]
        public async Task<ActionResult> GetComment(
        [FromQuery] PaginationIdRequest query)
        {
            var response = await _apiClient.BaseApi.CommentClient.GetAllAsync(query);

            return Ok(response);
        }

        [HttpPost("comment/like")]
        public async Task<ActionResult<IdResponse>> CreateCommentLike(
        [Required] Guid CommentId)
        {
            var response = await _apiClient.BaseApi.CommentClient.CreateCommentLikeAsync(CommentId);

            return Ok(response);
        }
        [HttpDelete("comment/like")]
        public async Task<ActionResult> DeleteCommentLike(
        [Required] Guid CommentId)
        {
            await _apiClient.BaseApi.CommentClient.DeleteCommentLikeAsync(CommentId);
            return Ok();
        }

        //Post

        [HttpPost("post")]
        public async Task<ActionResult<IdResponse>> CreatePost(
        [FromForm] CreatePostRequest request)
        {
            var response = await _apiClient.BaseApi.PostClient.CreatePostAsync(request);

            return Ok(response);
        }
        [HttpDelete("post")]
        public async Task<ActionResult> DeletePost(
        [FromForm] Guid PostId)
        {
            await _apiClient.BaseApi.PostClient.DeletePostAsync(PostId);

            return Ok();
        }
        [HttpGet("post")]
        public async Task<ActionResult<PaginationResponse<PostResponse>>> GetPosts(
        [FromQuery] PaginationIdRequest query)
        {
            var response = await _apiClient.BaseApi.PostClient.GetPostsAsync(query);

            return Ok(response);
        }
        [HttpPost("post/like")]
        public async Task<ActionResult<IdResponse>> CreatePostLike(
        [Required] Guid PostId)
        {
            var response = await _apiClient.BaseApi.PostClient.CreatePostLikeAsync(PostId);

            return Ok(response);
        }
        [HttpDelete("post/like")]
        public async Task<ActionResult> DeletePostLike(
        [Required] Guid PostId)
        {
            await _apiClient.BaseApi.PostClient.DeletePostLikeAsync(PostId);

            return Ok();
        }
        [HttpGet("post/like")]
        public async Task<ActionResult> GetLikedPosts(
        [FromQuery] PaginationIdRequest request)
        {

            var response = await _apiClient.BaseApi.PostClient.GetLikedPostsAsync(request);

            return Ok(response);
        }

        [HttpPost("post/pin")]
        public async Task<ActionResult<IdResponse>> PinPost(
      [Required] Guid PostId)
        {
            var response = await _apiClient.BaseApi.PostClient.PinPostAsync(PostId);

            return Ok(response);
        }
        [HttpDelete("post/pin")]
        public async Task<ActionResult> UnpinPost(
        [Required] Guid PostId)
        {
            await _apiClient.BaseApi.PostClient.UnpinPostAsync(PostId);

            return Ok();
        }
        [HttpGet("post/pin")]
        public async Task<ActionResult> GetPinnedPost(
        [FromQuery] PaginationIdRequest request)
        {

            var response = await _apiClient.BaseApi.PostClient.GetPinnedPostsAsync(request);

            return Ok(response);
        }

        [HttpPost("post/share")]
        public async Task<ActionResult<IdResponse>> SharePost(
     [Required] Guid PostId)
        {
            var response = await _apiClient.BaseApi.PostClient.SharePostAsync(PostId);

            return Ok(response);
        }
        [HttpDelete("post/share")]
        public async Task<ActionResult> DeleteSharedPost(
        [Required] Guid PostId)
        {
            await _apiClient.BaseApi.PostClient.DeleteSharedPostAsync(PostId);

            return Ok();
        }
        [HttpGet("post/share")]
        public async Task<ActionResult> GetSharedPosts(
        [FromQuery] PaginationIdRequest request)
        {

            var response = await _apiClient.BaseApi.PostClient.GetSharedPostsAsync(request);

            return Ok(response);
        }
    }
}