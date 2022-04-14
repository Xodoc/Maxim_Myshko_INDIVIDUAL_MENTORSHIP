using DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Subscription subscription);

        Task BulkSendEmailAsync(IGrouping<string, Subscription> subscriptions);
    }
}
