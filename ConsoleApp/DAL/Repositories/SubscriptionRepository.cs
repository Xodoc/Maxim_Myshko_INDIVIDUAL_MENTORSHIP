using DAL.Database;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Subscription> CreateSubscriptionByUserAsync(Subscription subscription)
        {
            var sub = await CreateAsync(subscription);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == sub.UserId);

            user.Subscriptions.Add(sub);

            await _context.SaveChangesAsync();

            return sub;
        }

        public async Task<List<Subscription>> CheckoutSubscriptionsAsync(int period)
        {
            var parameters = new SqlParameter("@period", period);

            return await _context.Subscriptions.FromSqlRaw("CheckoutSubs @period", parameters).ToListAsync();
        }

        public async Task<Subscription> RemoveSubscriptionByIdAsync(int subId) 
        {
            var sub = await _context.Subscriptions.FirstOrDefaultAsync(x => x.Id == subId);

            if (sub == null)
                return sub;

            sub.IsActive = false;

            await _context.SaveChangesAsync();

            return sub;
        }
    }
}
