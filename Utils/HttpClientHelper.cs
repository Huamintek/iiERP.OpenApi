using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iiERP.OpenApi.Utils
{
    public static class HttpClientHelper
    {
        /// <summary>
        /// Init Common Requset Parameters
        /// </summary>
        /// <param name="client"></param>
        /// <param name="appKey"></param>
        /// <param name="signToken"></param>
        /// <param name="content"></param>
        /// <param name="accessToken"></param>
        public static void InitCommonRequsetParameters(this HttpClient client, string appKey, string signToken, string content, string accessToken = null)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var timestamp = Convert.ToInt64(ts.TotalMilliseconds).ToString();
            Random rd = new Random(DateTime.Now.Millisecond);
            int i = rd.Next(0, int.MaxValue);
            var nonce = i.ToString();

            client.DefaultRequestHeaders.Add("timestamp", timestamp);
            client.DefaultRequestHeaders.Add("nonce", nonce);
            client.DefaultRequestHeaders.Add("appKey", appKey);
            client.DefaultRequestHeaders.Add("token", accessToken);
            client.DefaultRequestHeaders.Add("signature", Signature.GetSign(timestamp, nonce, appKey, signToken, content));
        }

        /// <summary>
        /// Concatenate GET parameters
        /// </summary>
        /// <param name="parames"></param>
        /// <returns></returns>
        public static Tuple<string, string> GetQueryString(Dictionary<string, string> parames)
        {
            // Sort parameter
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parames);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // connect parameters with &
            StringBuilder query = new StringBuilder("");
            StringBuilder queryStr = new StringBuilder("");
            if (parames == null || parames.Count == 0)
                return new Tuple<string, string>("", "");

            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = System.Web.HttpUtility.UrlEncode(dem.Current.Value);
                if (!string.IsNullOrEmpty(key))
                {
                    query.Append(key).Append(value);
                    queryStr.Append("&").Append(key).Append("=").Append(value);
                }
            }

            return new Tuple<string, string>(query.ToString(), queryStr.ToString().Substring(1, queryStr.Length - 1));
        }

    }
}
