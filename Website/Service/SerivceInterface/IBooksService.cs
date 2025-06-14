using Domain.Models.ModelDTOs;

namespace Website.Service.SerivceInterface
{
    public interface IBooksService
    {
        Task<T> GetAsync<T>();
        Task<T> GetById<T>(int id);
        Task<T> CreateAync<T>(BooksDTO dto);
        Task<T> UpdateAync<T>(BooksDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
