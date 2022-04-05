﻿using BL.Interfaces;
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
        /// Get report from some period by city names and period
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Success</response>
        /// <response code="400">If send data is incorrect</response>
        /// <response code="500">Internal server error</response>
        /// <returns></returns>
        [HttpPost("getReportFromPeriodByCityNames")]
        public async Task<IActionResult> GetReport([FromBody] ReportRequest request) 
        {
            var period = DateTime.Now.Subtract(request.FromDate);
            
            return Ok(await _reportService.CreateReportAsync(request.CityNames, period));
        }
    }
}
