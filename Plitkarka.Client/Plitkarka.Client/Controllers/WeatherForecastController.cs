using Microsoft.AspNetCore.Mvc;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Authorization;
using Plitkarka.Client.Models.Pagination;
using Plitkarka.Client.Models.ResetPassword;
using Plitkarka.Client.Models.User;

namespace Plitkarka.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private IApiClient _apiClient { get; init; }

        public Controller(
            IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpPost("user/image")]
        public async Task<ActionResult<IdResponse>> SendUserImage([FromForm] SetUserImageRequestModel body)
        {
            var response = await _apiClient.UserClient.SetUserImage(body);

            return Ok(response);
        }

        [HttpGet("user/image")]
        public async Task<ActionResult<StringResponse>> GetUserImageURL(
        [FromQuery] Guid userId)
        {
            var response = await _apiClient.UserClient.GetImageUrlByUserId(userId);

            return Ok(response);
        }
       
        [HttpGet("user")]
        public async Task<ActionResult<UserData>> GetUserData(
        [FromQuery] Guid userId)
        {
            var response = await _apiClient.UserClient.GetByIdAsync(userId);

            return Ok(response);
        }

        [HttpGet("user/all")]
        public async Task<ActionResult<PaginationResponse<UserPreview>>> GetAllUserData(
            [FromQuery] PaginationTextRequest body)
        {
            var response = await _apiClient.UserClient.GetAll(body);

            return Ok(response);
        }

        //Auth

        [HttpPost("auth")]
        public async Task<ActionResult<StringResponse>> SingUp(
            [FromBody] SignUpRequest body)
        {
            var response = await _apiClient.AuthClient.SignUp(body);

            return Ok(response);
        }

        [HttpPost("auth/signin")]
        public async Task<ActionResult<TokenPairResponse>> SingIn(
          [FromBody] SignInRequest body)
        {
            var response = await _apiClient.AuthClient.SignIn(body);

            return Ok(response);
        }

        [HttpPost("email")]
        public async Task<ActionResult<TokenPairResponse>> VerifyEmail(
          [FromBody] VerifyEmailRequest body)
        {
            var response = await _apiClient.AuthClient.VerifyEmail(body);

            return Ok(response);
        }

        [HttpPut("email")]
        public async Task<ActionResult<StringResponse>> ResendVerificationCode(
         [FromBody] ResendVerificationCodeRequest body)
        {
            var response = await _apiClient.AuthClient.ResendVerificationCode(body);

            return Ok(response);
        }

        [HttpGet("refresh")]
        public async Task<ActionResult<TokenPairResponse>> RefreshTokenPair(
         [FromQuery] string refreshToken)
        {
            var response = await _apiClient.AuthClient.GetNewTokenPair(refreshToken);

            return Ok(response);
        }

        //Reset

        [HttpPost("password/reset")]
        public async Task<ActionResult<StringResponse>> SendEmail(
         [FromBody] SendEmailRequest email)
        {
            var response = await _apiClient.ResetPasswordClient.SendEmail(email);

            return Ok(response);
        }

        [HttpGet("password/reset")]
        public async Task<ActionResult<VerifyCodeRequest>> VerifyCode(
         [FromQuery] VerifyCodeRequest body)
        {
            var response = await _apiClient.ResetPasswordClient.VerifyCode(body);

            return Ok(response);
        }

        [HttpPut("password/reset")]
        public async Task<ActionResult<TokenPairResponse>> ResetPassword(
         [FromBody] ResetPasswordRequest body)
        {
            var response = await _apiClient.ResetPasswordClient.ResetPassword(body);

            return Ok(response);
        }

        //Sub

        [HttpPost("sub")]
        public async Task<ActionResult<IdResponse>> Subscribe(
         [FromQuery] Guid SubscribeToId)
        {
            var response = await _apiClient.SubscriptionClient.Subscribe(SubscribeToId);

            return Ok(response);
        }

        [HttpDelete("sub")]
        public async Task<ActionResult<IdResponse>> Unsubscribe(
         [FromQuery] Guid UnsubscribeFromId)
        {
            var response = await _apiClient.SubscriptionClient.Unsubscribe(UnsubscribeFromId);

            return Ok(response);
        }

        [HttpGet("sub/subscribers/all")]
        public async Task<ActionResult<TokenPairResponse>> GetSubscribers(
             [FromQuery] Guid filter, int page)
        {
            var response = await _apiClient.SubscriptionClient.GetAllSubscribers(new PaginationIdRequest() 
            { Id = filter, Page = page});

            return Ok(response);
        }

        [HttpGet("sub/subscription/all")]
        public async Task<ActionResult<TokenPairResponse>> GetSubscription(
             [FromQuery] Guid filter, int page)
        {
            var response = await _apiClient.SubscriptionClient.GetAllSuscriptions(new PaginationIdRequest()
            { Id = filter, Page = page });

            return Ok(response);
        }
    }
}