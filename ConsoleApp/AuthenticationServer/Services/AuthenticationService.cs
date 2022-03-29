using AuthenticationServer.Interfaces;
using BL.Interfaces;

namespace AuthenticationServer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public async Task<string> Authenticate(string email, string password) 
        {
            var user = await _userService.GetUserByUserCredentialsAsync(email, password);

            if (user == null) 
            {
                return string.Empty;
            }

            return await _tokenService.GetToken(user);
        }
    }
}
