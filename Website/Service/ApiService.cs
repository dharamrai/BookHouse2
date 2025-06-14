using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Website.Service.SerivceInterface;
using Website.WebModels;

namespace Website.Service
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpclient;
        private readonly IConfiguration _config;
        public ApiService(IHttpClientFactory httpclient, IConfiguration config)
        {
            _config = config;
            _httpclient = httpclient;
        }

        public async Task<T> SendRequestAsync<T>(ApiRequest request)
        {
            var client = _httpclient.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(100);

            string baseUrl = _config["ApiUrl"];
            string fullUrl = $"{baseUrl.TrimEnd('/')}/{request.Url.TrimStart('/')}";

            var httpRequest = new HttpRequestMessage(request.Method, fullUrl);

            if (request.Data != null && request.Method != HttpMethod.Get)
            {
                string jsonData = JsonConvert.SerializeObject(request.Data);
                httpRequest.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            }

            using var response = await client.SendAsync(httpRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<T>(responseContent);

            return apiResult;
        }

    }
}
