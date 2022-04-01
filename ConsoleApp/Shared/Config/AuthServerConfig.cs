using Microsoft.IdentityModel.Tokens;
using Shared.Interfaces;

namespace Shared.Config
{
    public class AuthServerConfig : IAuthServerConfig
    {
        public SigningCredentials SigningCredentials { get; set; }
    }
}
