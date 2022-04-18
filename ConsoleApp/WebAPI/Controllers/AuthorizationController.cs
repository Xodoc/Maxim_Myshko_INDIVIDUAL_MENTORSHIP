using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;
using WebAPI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthorizationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Getting access token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, Application.Json);

            var authenticationClient = _httpClientFactory.CreateClient();

            var responseMessage = await authenticationClient.PostAsync("https://localhost:5001/api/Authentication/GetAccessToken", content);

            var token = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized(token);
            }
            if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                return StatusCode(500);
            }

            return Ok(token);
        }
    }
}
