using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Conext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class BooksRepository : Repository<Books>, IBooksRepository
    {
        public BooksRepository(AppDbContext dbcontext):base(dbcontext)
        {            
        }

        public async Task<string> CheckDuplicate(string bookname, string author)
        {
           string checkDup = string.Empty;
            var result = await _dbcontext.Books.Where(x => x.BookName == bookname && x.Author == author).FirstOrDefaultAsync();
            if (result != null)
            {
                checkDup = "Y";
                return checkDup;
            }
            else
            {
                checkDup = "N";
                return checkDup;
            }
        }
    }
}
