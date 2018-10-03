using FC_MVVC.Helpers.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_MVVC.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<EmailSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public EmailSettings Options { get; } 

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.Password, subject, message, email);
        }

        public Task Execute(string password, string subject, string message, string email)
        {
            var client = new SendGridClient(password);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("fitnesscommunity.info@gmail.com", "Fitness Community"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.TrackingSettings = new TrackingSettings
            {
                ClickTracking = new ClickTracking { Enable = false }
            };

            return client.SendEmailAsync(msg);
        }
    }
}
