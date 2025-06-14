using Website.WebModels;

namespace Website.Service.SerivceInterface
{
    public interface IApiService
    {
        Task<T> SendRequestAsync<T>(ApiRequest request);
    }
}
