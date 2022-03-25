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

        /// <summary>
        /// Getting access token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>

        [HttpPost("Authorization")]
        public async Task<IActionResult> Authorization([FromQuery] AuthenticationRequest request)
        {
            var token = await _authorizationService.AuthenticationAsync(request.Email, request.Password);

            if (string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized("Invalid login or password.");
            }

            return Ok(token);
        }
    }
}
