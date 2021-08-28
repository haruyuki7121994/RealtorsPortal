using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using src.Config;
using src.Models;

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
                    $"Hello! Thanks for register account!\n Please click below link to complete register account. \n {confirmUrl}\nRealtors Portal."
                );
        }

        public bool SendEmailExpiredNotification(Package package, string customerEmail)
        {
            return SendEmail
                (
                    customerEmail,
                    "[Realtors Portal] Package Expired!",
                    $"We would like to inform you that your package [{package.Name}] has expired!\n You can sign up for renewal or buy for a new package. \n Thank you for using our service!\nRealtors Portal."
                );
        }

        public bool SendEmailPaymentPackage(PaymentPackage paymentPackage)
        {
            return SendEmail
                (
                    paymentPackage.Customer.Name,
                    "[Realtors Portal] Payment Package!",
                    $"Thank you for purchasing the package [{paymentPackage.Package.Name}]!\n Price: {paymentPackage.Package.Price} \n Limit Ads: {paymentPackage.Limit_ads} \n Limit featured ads: {paymentPackage.Limit_featured_ads} \n Your package will be confirmed by us within 4 hours.\n Thank you for using our service!\nRealtors Portal."
                );
        }

        public bool SendEmailPaymentSubscription(PaymentSubscription paymentSubscription)
        {
            return SendEmail
                (
                    paymentSubscription.Customer.Name,
                    "[Realtors Portal] Payment Subscription!",
                    $"Thank you for paid subscription!\n Price: {paymentSubscription.Payment_price} \n Thank you for using our service!\nRealtors Portal."
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
