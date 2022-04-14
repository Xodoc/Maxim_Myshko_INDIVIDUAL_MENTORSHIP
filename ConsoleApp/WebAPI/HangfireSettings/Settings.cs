using BL.Interfaces;
using Shared.Parsers;
using Hangfire;

#nullable disable

namespace WebAPI.HangfireSettings
{
    public class Settings
    {
        private readonly IConfiguration _config;
        private readonly ISubscriptionService _subscriptionService;

        public Settings(IConfiguration config, ISubscriptionService subscriptionService)
        {
            _config = config;
            _subscriptionService = subscriptionService;
        }

        public async Task SetTasksAsync()
        {
            var subs = await _subscriptionService.GetAllSubscriptionsAsync();

            var groups = subs.GroupBy(x => x.Cron).ToList();

            var crons = _config.GetSection("CronSettings").GetChildren().Select(x => x.Value).ToArray();

            foreach (var group in groups)
            {
                var num = groups.IndexOf(group);

                RecurringJob.AddOrUpdate<IEmailService>($"sendmessage_{num}", x => x.BulkSendEmailAsync(group), GetCron(crons, group.Key));
                RecurringJob.AddOrUpdate($"checkoutsubs_{num}", () => CheckoutGroupSubsByCronAsync(group.Key, num), Cron.Hourly);
            }
        }

        private async Task CheckoutGroupSubsByCronAsync(string cron, int messageNumber)
        {
            var subs = await _subscriptionService.CheckoutSubscriptionsAsync(CronParser.ParseCronToHours(cron));

            if (subs.Count > 0 && subs != null)
            {
                RecurringJob.Trigger($"sendmessage{messageNumber}");
            }
        }

        private string GetCron(string[] crons, string period)
        {
            switch (period)
            {
                case "Every hour": return crons[0];
                case "Every 3 hours": return crons[1];
                case "Every 12 hours": return crons[2];
                case "Daily": return crons[3];
                default: return Cron.Daily();
            }
        }
    }
}
