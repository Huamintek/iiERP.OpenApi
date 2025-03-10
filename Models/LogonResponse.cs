using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iiERP.OpenApi.Models
{
    public class LogonResponse
    {
        public string token { get; set; }
        public LogonUserInfo logonUserInfo { get; set; }
        public string lang { get; set; }
    }
}
