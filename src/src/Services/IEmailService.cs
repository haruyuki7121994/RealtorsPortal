﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    public interface IEmailService
    {
        bool SendEmailRegisterAccount(string confirmUrl, string customerEmail);
    }
}
