using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBooksRepository:IRepository<Books>
    {
        Task<string> CheckDuplicate(string bookname, string author);
    }
}
