using Microsoft.AspNetCore.Mvc;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;
using Plitkarka.Client.Models.Authorization;
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
        public async Task<ActionResult<PaginationResponse<UserPreview>>> GetAllUserData()
        {
            var response = await _apiClient.UserClient.GetAll();

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
    }
}