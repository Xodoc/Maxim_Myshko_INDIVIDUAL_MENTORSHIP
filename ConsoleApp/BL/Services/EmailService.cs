using BL.Interfaces;
using DAL.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using Serilog;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IReportService _reportService;

        private const string AdminEmail = "maks.myshko.99@mail.ru";
        private const string AdminPassword = "XhLjtxdWHNV59bHa1F70";
        private const string Subject = "Report";

        public EmailService(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<bool> SendEmailAsync(Subscription sub)
        {
            var reportMessage = await GenerateReportAsync(sub);

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Admin", AdminEmail));
            emailMessage.To.Add(new MailboxAddress("", sub.User.Email));
            emailMessage.Subject = Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = reportMessage
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                await client.AuthenticateAsync(AdminEmail, AdminPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }

            Log.Information($"Message has been sent to the mail {sub.User.Email}");

            return true;
        }

        public async Task BulkSendEmailAsync(IGrouping<string, Subscription> subscriptions)
        {
            foreach (var sub in subscriptions)
            {
                if (sub.IsActive == true)
                {
                    await SendEmailAsync(sub);
                }
            }
        }

        private async Task<string> GenerateReportAsync(Subscription sub)
        {
            return await _reportService.CreateReportAsync(sub.Id, sub.FromDate);
        }
    }
}
