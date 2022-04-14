using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        //[HttpGet("sendMessage")]
        //public async Task<IActionResult> SendMessage([FromQuery] string email)
        //{
        //    var result = await _emailService.SendEmailAsync(email);

        //    if (result == false)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //    return Ok("Message has been sent!");
        //}
    }
}
