using Domain.Models;
using Website.WebModels;

namespace Website.Service.SerivceInterface
{
    public interface IBaseService
    {
        ApiResponse responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
