using AuthenticationServer.Models;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthenticationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("GetAccessToken")]
        public async Task<IActionResult> GetAccessToken([FromBody] AuthenticationRequest request)
        {
            var token = await _authorizationService.AuthenticationAsync(request.Email, request.Password);

            return Ok(token);
        }
    }
}
