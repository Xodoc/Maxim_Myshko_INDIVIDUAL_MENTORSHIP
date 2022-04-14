using BL.DTOs;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ISubscriptionService
    {
        Task<List<Subscription>> GetAllSubscriptionsAsync();

        Task<List<Subscription>> CheckoutSubscriptionsAsync(int period);

        Task<SubscriptionDTO> CreateSubscriptionByUserAsync(SubscriptionDTO subscriptionDto);

        Task<SubscriptionDTO> RemoveSubscriptionByIdAsync(int subscriptionId);
    }
}
