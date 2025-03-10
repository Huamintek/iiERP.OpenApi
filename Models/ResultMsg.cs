using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iiERP.OpenApi.Models
{
    public class ResultMsg<T>
    {
        /// <summary>
        /// Status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Information
        /// </summary>
        public object Info { get; set; }

        /// <summary>
        /// Response datas
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Total count
        /// </summary>
        public int Total { get; set; }

    }
}
