using Microsoft.AspNetCore.Mvc;
using Plitkarka.Client.Interfaces;
using Plitkarka.Client.Models;

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

        [HttpPost("image")]
        public async Task<ActionResult<IdResponse>> SendUserImage([FromForm] SetUserImageRequestModel body)
        {
            var response = await _apiClient.UserClient.SetUserImage(body);

            return Ok(response);
        }

        [HttpGet("image")]
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

        [HttpGet("all")]
        public async Task<ActionResult<PaginationResponse<UserPreview>>> GetAllUserData()
        {
            var response = await _apiClient.UserClient.GetAll();

            return Ok(response);
        }
    }
}