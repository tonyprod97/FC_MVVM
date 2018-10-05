using FC_MVVC.Helpers.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
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

        public Task SendEmailAsync(string toAdress, string subject, string body)
        {
            return Execute(Options.Sender, Options.Password, subject, body, toAdress);
        }

        public Task Execute(string sender, string password, string subject, string body, string toAdress)
        {
            return Task.Run(()=>
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("FittnesCommunity", sender));
                message.To.Add(new MailboxAddress(toAdress));

                message.Subject = "Welcome to Fitness Community";
                message.Body = new TextPart("plain")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(sender, password);
                    client.Send(message);
                    client.Disconnect(true);
                }
            });
        }
    }
}
