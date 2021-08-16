using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Config
{
    public class EmailSettings
    {
        public const string SectionName = "EmailSettings";

        public string MailDriver { get; set; }
        public int Port { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }
    }
}
