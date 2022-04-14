using AuthenticationServer.Interfaces;
using BL.Interfaces;
using DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthenticationServer.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly Shared.Interfaces.IAuthServerConfig _authServerConfig;

        public TokenService(IConfiguration config, IUserService userService, Shared.Interfaces.IAuthServerConfig authServerConfig)
        {
            _config = config;
            _userService = userService;
            _authServerConfig = authServerConfig;
        }

        public async Task<string> GetToken(User user) 
        {
            var tokenDescriptor = await GetTokenDescriptor(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(securityToken);
        }

        private async Task<SecurityTokenDescriptor> GetTokenDescriptor(User user)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(await _userService.GetUserClaims(user)),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                NotBefore = DateTime.UtcNow,               
                Expires = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:LifeTimeInDays"])),
                SigningCredentials = _authServerConfig.SigningCredentials
            };
        }
    }
}
