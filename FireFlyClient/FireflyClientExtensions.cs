using System;

namespace FireflyIII
{
    public partial class Client : BaseClient
    {
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            if (!string.IsNullOrEmpty(Config.ApiToken) && !request.Headers.Contains("Authorization"))
            {
                request.Headers.Add("Authorization", "Bearer " + Config.ApiToken);
            }

            request.RequestUri = new Uri(url.Replace(BaseUrl, Config.BaseUrl));
        }
    }
}
