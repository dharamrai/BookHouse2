using Domain.Models.ModelDTOs;
using Website.Service.SerivceInterface;
using Website.WebModels;

namespace Website.Service
{
    public class BooksService : BaseService, IBooksService
    {
        private readonly IHttpClientFactory _httpclient;
        private string apiUrl;
        public BooksService(IHttpClientFactory httpclient, IConfiguration config):base(httpclient)
        {
            _httpclient = httpclient;
            apiUrl = config.GetValue<string>("ApiUrl");
        }

        public Task<T> GetAsync<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                Method = HttpMethod.Get,
                Url = apiUrl + "/api/BooksApi"
            });
        }

        public Task<T> GetById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> CreateAync<T>(BooksDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAync<T>(BooksDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

    }
}
