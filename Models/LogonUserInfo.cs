using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iiERP.OpenApi.Models
{
    public class LogonUserInfo
    {
        public bool IsMgmtDomain { get; set; }
        public string MFViewStyle { get; set; }
        public string userLogo { get; set; }
        public string bu { get; set; }
        public string user { get; set; }
        public string langMstrRK { get; set; }
        public int buNumber { get; set; }
        public string currId { get; set; }

        public string ip { get; set; }

        public string userAgent { get; set; }

        public DateTime logonTime { get; set; }

        public DateTime activeTime { get; set; }
        public string appName { get; set; }
        public string appId { get; set; }


        public string userType { get; set; }

        public string companyId { get; set; }

        public decimal longitude { get; set; }

        public decimal latitude { get; set; }

        public string locationName { get; set; }

        public string token { get; set; }
        /// <summary>
        /// Type：BU、APP、Community
        /// </summary>
        public string type { get; set; }
    }

}
