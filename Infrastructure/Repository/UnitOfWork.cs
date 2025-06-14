using Domain.Interfaces;
using Infrastructure.Conext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBooksRepository BooksRepository { get; }
        private readonly AppDbContext _dbcontext;
        public UnitOfWork(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;   
            BooksRepository = new BooksRepository(dbcontext);
        }

      

        public void Dispose()
        {
           _dbcontext.Dispose();
        }

        public Task<int> SaveChanges()
        {
            return _dbcontext.SaveChangesAsync();
        }
    }
}
