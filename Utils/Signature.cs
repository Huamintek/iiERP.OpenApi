using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace iiERP.OpenApi.Utils
{
    /// <summary>
    /// Signature class
    /// </summary>
    public static class Signature
    {
        /// <summary>
        /// Get sign
        /// </summary>
        /// <param name="timeStamp">TimeStamp</param>
        /// <param name="nonce">Nonce</param>
        /// <param name="appKey">AppKey, optional</param>
        /// <param name="token">Token,optional</param>
        /// <param name="data">Request data</param>
        /// <returns></returns>
        public static string GetSign(string timeStamp, string nonce, string appKey, string token, string data)
        {
            var hash = System.Security.Cryptography.MD5.Create();
            var signStr = timeStamp + nonce + appKey + token + data;
            var sortStr = string.Concat(signStr.OrderBy(c => c));
            var bytes = Encoding.UTF8.GetBytes(sortStr);
            var md5Val = hash.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            foreach (var c in md5Val)
            {
                result.Append(c.ToString("X2"));
            }

            return result.ToString().ToUpper();
        }

    }
}
