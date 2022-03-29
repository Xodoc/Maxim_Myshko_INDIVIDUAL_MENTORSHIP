using BL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityUser> GetUserByUserCredentialsAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }

            return null;
        }

        public async Task<List<Claim>> GetUserClaims(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //var rsaProvider = new RSACryptoServiceProvider(1024);
            //var rsaKey = new RsaSecurityKey(rsaProvider);

            //var token = new JwtSecurityToken(
            //_config["Jwt:Issuer"],
            //_config["Jwt:Audience"],
            //notBefore: DateTime.UtcNow,
            //claims: authClaims,
            //expires: DateTime.UtcNow.Add(TimeSpan.FromDays(int.Parse(_config["Jwt:LifeTimeInDays"]))),
            //signingCredentials: new SigningCredentials(rsaKey, SecurityAlgorithms.RsaSha512Signature));

            //return new JwtSecurityTokenHandler().WriteToken(token);

            return authClaims;
        }
    }
}
