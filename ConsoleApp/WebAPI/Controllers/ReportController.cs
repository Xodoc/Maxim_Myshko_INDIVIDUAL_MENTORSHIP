using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Get report from some period by subscription id and period
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Success</response>
        /// <response code="400">If send data is incorrect</response>
        /// <response code="500">Internal server error</response>
        /// <returns></returns>
        [HttpPost("getReportFromPeriodBySubscriptionId")]
        public async Task<IActionResult> GetReport([FromBody] ReportRequest request)
        {
            return Ok(await _reportService.CreateReportAsync(request.SubscriptionId, request.FromDate));
        }
    }
}
