using Shared.Certificates;
using AuthenticationServer.Interfaces;
using BL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthenticationServer.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly SigningAudienceCertificate _certificate;

        public TokenService(IConfiguration config, IUserService userService, SigningAudienceCertificate certificate)
        {
            _config = config;
            _userService = userService;
            _certificate = certificate;
        }

        public async Task<string> GetToken(IdentityUser user) 
        {
            var tokenDescriptor = await GetTokenDescriptor(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        private async Task<SecurityTokenDescriptor> GetTokenDescriptor(IdentityUser user)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(await _userService.GetUserClaims(user)),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                NotBefore = DateTime.UtcNow,               
                Expires = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:LifeTimeInDays"])),
                SigningCredentials = _certificate.GetAudienceSigningKey()
            };
        }
    }
}
