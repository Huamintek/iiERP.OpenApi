using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using iiERP.OpenApi.Models;
using iiERP.OpenApi.Utils;
using System.Net.Http;

/*
 * This is the project that privoides guidance on how to request Huamintek API interface
 * Reference: https://api-doc.iierp.com
 */

//Initialization parameters
var host = "<Huamintek host url>";
string appKey = "<Your app key here>";

#region 1. Get key Info
//Get key info by appKey value
var signToken = string.Empty;
var url = $"{host}/ycmn/api/token/getToken";
using (HttpClient client = new HttpClient())
{
    var queryParams = new Dictionary<string, string>
                {
                    { "appKey", appKey }
                };
    var queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
    var requestUrl = $"{url}?{queryString}";
    HttpResponseMessage tokenResponse = await client.GetAsync(requestUrl);
    string responseBody = await tokenResponse.Content.ReadAsStringAsync();
    var resultMsg = JsonSerializer.Deserialize<ResultMsg<Token>>(responseBody);
    signToken = resultMsg?.Data?.SignToken.ToString();
}

#endregion

#region 1. Get access token
//Get access token by user name and password
url = $"{host}/ysso/api/account/login";
var accessToken = string.Empty;

using (HttpClient client = new HttpClient())
{
    var requestBody = new
    {
        user = "<User Name>",
        pwd = "<User Password>"
    };
    var jsonBody = JsonSerializer.Serialize(requestBody);
    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

    //Init Common Requset Parameters
    client.InitCommonRequsetParameters(appKey, signToken, jsonBody);

    var response = await client.PostAsync(url, content);

    if (response.IsSuccessStatusCode)
    {
        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Response:" + responseBody);

        var resultMsg = JsonSerializer.Deserialize<ResultMsg<object>>(responseBody);
        if (resultMsg?.StatusCode == 200)
        {
            var data = JsonSerializer.Deserialize<LogonResponse>(resultMsg.Data.ToString());
            accessToken = data?.token;
        }
        else
        {
            Console.WriteLine("Response Error:" + resultMsg?.Info);
        }
    }
    else
    {
        Console.WriteLine($"Request failed，error code: {response.StatusCode}");
    }
}
#endregion

#region 2. Request API interface demo
//Get logon user infomation by accesstoken
if (!string.IsNullOrWhiteSpace(accessToken))
{
    using (HttpClient client = new HttpClient())
    {
        url = $"{host}/api/User/GetUserInfo";
        var queryParams = new Dictionary<string, string>{ 
            { "acctId", "<Your account ID>" } //acctId attribute from login interface
        };
        var queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
        var requestUrl = $"{url}?{queryString}";
        var queryData = HttpClientHelper.GetQueryString(queryParams);
        client.InitCommonRequsetParameters(appKey, signToken, queryData.Item1, accessToken);
        HttpResponseMessage tokenResponse = await client.GetAsync(requestUrl);
        string responseBody = await tokenResponse.Content.ReadAsStringAsync();
        //var resultMsg = JsonSerializer.Deserialize<ResultMsg<object>>(responseBody);
        Console.WriteLine($"Get User Info: {responseBody}");
    }
}
#endregion
