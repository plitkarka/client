using Microsoft.AspNetCore.Http;

namespace Plitkarka.Client.Models;

public record SetUserImageRequestModel(IFormFile Image);