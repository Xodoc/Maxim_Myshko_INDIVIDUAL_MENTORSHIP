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

        [HttpPost("getReportFromPeriodByCityNames")]
        public async Task<IActionResult> GetReport([FromBody] ReportRequest request) 
        {
            var period = DateTime.Now.Subtract(request.FromDate);
            
            return Ok(await _reportService.CreateReportAsync(request.CityNames, period));
        }
    }
}
