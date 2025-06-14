using Domain.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Website.Service.SerivceInterface;
using Website.WebModels;

namespace Website.Service
{
    public class BaseService : IBaseService
    {
        public ApiResponse responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpclient)
        {
            this.responseModel = new();
            this.httpClient = httpclient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("BooksApi");

                var message = new HttpRequestMessage
                {
                    Method = apiRequest.Method,
                    RequestUri = new Uri(apiRequest.Url)
                };

                message.Headers.Add("Accept", "application/json");

                if (apiRequest.Data != null &&
                    (apiRequest.Method == HttpMethod.Post || apiRequest.Method == HttpMethod.Put))
                {
                    message.Content = new StringContent(
                        JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8,
                        "application/json"
                    );
                }

                using var apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }
            catch (Exception ex)
            {
                var dto = new ApiResponse
                {
                    Errors = new List<string> { ex.Message },
                    IsSuccess = false,
                };

                var errorJson = JsonConvert.SerializeObject(dto);
                return JsonConvert.DeserializeObject<T>(errorJson);
            }

        }
    }
}
