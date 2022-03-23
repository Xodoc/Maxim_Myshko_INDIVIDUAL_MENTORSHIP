using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<string> AuthenticationAsync(string email, string password);
    }
}
