using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailOTP.Domain.Models
{
    public class EmailSettings
    {
        public string MailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
