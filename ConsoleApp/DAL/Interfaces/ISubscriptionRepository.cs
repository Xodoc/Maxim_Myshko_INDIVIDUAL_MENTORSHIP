using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISubscriptionRepository : IGenericRepository<Subscription>
    {
        Task<Subscription> CreateSubscriptionByUserAsync(Subscription subscription);

        Task<List<Subscription>> CheckoutSubscriptionsAsync(int period);

        Task<Subscription> RemoveSubscriptionByIdAsync(int subscriptionId);
    }
}
