using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using src.Config;

namespace src.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSettings> options;

        public EmailService(IOptions<EmailSettings> options)
        {
            this.options = options;
        }

        public bool SendEmailRegisterAccount(string confirmUrl, string customerEmail)
        {
            return SendEmail
                (
                    customerEmail,
                    "[Realtors Portal] Account verification email!",
                    $"Hello! Thanks for register account!\n Please click below link to complete register account. \n {confirmUrl}"
                );
        }

        private bool SendEmail(string receiver, string subject, string message)
        {
            try
            {
                var senderEmail = new MailAddress(options.Value.FromAddress, options.Value.FromName);
                var receiverEmail = new MailAddress(receiver, "Receiver");
                var password = options.Value.Password;
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = options.Value.MailDriver,
                    Port = options.Value.Port,
                    EnableSsl = options.Value.EnableSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
