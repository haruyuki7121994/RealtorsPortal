using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IEmailService
    {
        bool SendEmailRegisterAccount(string confirmUrl, string customerEmail);
        bool SendEmailExpiredNotification(Package package, string customerEmail);
    }
}
