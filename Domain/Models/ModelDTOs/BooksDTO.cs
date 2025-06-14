using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ModelDTOs
{
    public class BooksDTO
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string BookImage { get; set; }
        public IFormFile BookImageFile { get; set; }
    }
}
