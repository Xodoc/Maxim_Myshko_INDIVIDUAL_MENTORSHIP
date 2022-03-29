using AuthenticationServer.Interfaces;
using AuthenticationServer.Models;
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

        /// <summary>
        /// Getting access token by email and password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("GetAccessToken")]
        public async Task<IActionResult> GetAccessToken([FromBody] AuthenticationRequest request)
        {
            var token = await _authorizationService.Authenticate(request.Email, request.Password);

            if (string.IsNullOrWhiteSpace(token)) 
            {
                return Unauthorized("Invalid email or password!");
            }

            return Ok(token);
        }
    }
}
