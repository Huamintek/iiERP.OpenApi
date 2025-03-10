using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iiERP.OpenApi.Models
{
    public class Token
    {
        /// <summary>
        /// app key
        /// </summary>
        public string appKey { get; set; }

        /// <summary>
        /// key Token
        /// </summary>
        public Guid SignToken { get; set; }


        /// <summary>
        /// Token expire time
        /// </summary>
        public DateTime ExpireTime { get; set; }
    }
}
