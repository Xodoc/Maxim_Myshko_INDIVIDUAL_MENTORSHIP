using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<string> AuthorizationAsync(string email, string password);
    }
}
