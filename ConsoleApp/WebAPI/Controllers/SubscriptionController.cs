using BL.DTOs;
using BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Parsers;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        /// <summary>
        /// Created subscription by user and specific parameters of subscription 
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("createSubscriptionByUser")]
        public async Task<IActionResult> CreateSubscriptionByUser([FromForm] SubscriptionRequest request)
        {
            var cron = CronParser.ParseHoursToCron(request.Periodicity);
            var subDto = new SubscriptionDTO
            {
                Cron = cron,
                IsActive = request.IsActive,
                UserId = request.UserId,
                FromDate = request.FromDate
            };

            var responseMessage = await _subscriptionService.CreateSubscriptionByUserAsync(subDto);

            return Ok(responseMessage);
        }

        /// <summary>
        /// Remove subscription by subscription id
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <response code="200">Success</response>
        /// <response code="204">No content</response>
        /// <response code="500">Internal server error</response>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("removeSubscroptionById")]
        public async Task<IActionResult> RemoveSubscriptionById([FromQuery] int subscriptionId) 
        {            
            return Ok(await _subscriptionService.RemoveSubscriptionByIdAsync(subscriptionId));
        }
    }
}
