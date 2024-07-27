using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class SmtpSettings
    {
        public string Server { get; set; }=string.Empty;
        public int Port { get; set; }
        public string SenderName { get; set; }=string.Empty;
        public string SenderEmail { get; set; }=string.Empty;
        public string Username { get; set; }=string.Empty;
        public string Password { get; set; }=string.Empty;
        public bool EnableSsl { get; set; }
    }
}