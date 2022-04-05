using BL.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IReportService _reportService;
        private readonly IConfiguration _config;

        private const string AdminEmail = "maks.myshko.99@mail.ru";
        private const string AdminPassword = "XhLjtxdWHNV59bHa1F70";
        private const string Subject = "Report";

        public EmailService(IReportService reportService, IConfiguration config)
        {
            _reportService = reportService;
            _config = config;
        }

        public async Task<bool> SendEmailAsync(string email)
        {
            var reportMessage = await GenerateReportAsync();

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Admin", AdminEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
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

            Log.Information($"Message has been sent to the mail {email}");

            return true;
        }

        private async Task<string> GenerateReportAsync()
        {
            var cities = _config.GetSection("CitiesForReport").GetChildren().Select(x => x.Value);
            var fromDate = DateTime.Parse(_config["FromDate"]);

            var reportMessage = await _reportService.CreateReportAsync(cities, fromDate);

            return reportMessage;
        }
    }
}
