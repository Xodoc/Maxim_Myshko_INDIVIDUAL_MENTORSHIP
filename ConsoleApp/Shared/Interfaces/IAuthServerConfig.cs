using Microsoft.IdentityModel.Tokens;

namespace Shared.Interfaces
{
    public interface IAuthServerConfig
    {
        SigningCredentials SigningCredentials { get; }
    }
}
