﻿using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthorizationRequest request) 
        {
            var token = await _authorizationService.AuthorizationAsync(request.Email, request.Password);

            if (string.IsNullOrWhiteSpace(token)) 
            {
                return Unauthorized("Invalid login or password.");
            }

            return Ok(token);
        }
    }
}
