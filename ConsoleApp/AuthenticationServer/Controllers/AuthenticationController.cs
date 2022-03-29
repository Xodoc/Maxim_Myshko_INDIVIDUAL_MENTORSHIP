using AuthenticationServer.Interfaces;
using AuthenticationServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("GetAccessToken")]
        public async Task<IActionResult> GetAccessToken([FromBody] AuthenticationRequest request)
        {
            var token = await _authenticationService.Authenticate(request.Email, request.Password);

            return Ok(token);
        }
    }
}
