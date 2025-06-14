using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<List<TModel>> Get();
        Task<TModel> Get(int id);
        void Create(TModel model);
        void Update(TModel model);
        void Delete(TModel model);
    }
}
