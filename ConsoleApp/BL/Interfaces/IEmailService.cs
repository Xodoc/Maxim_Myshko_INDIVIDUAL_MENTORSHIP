using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email);
    }
}
