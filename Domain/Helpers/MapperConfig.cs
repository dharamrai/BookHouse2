using AutoMapper;
using Domain.Models;
using Domain.Models.ModelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Books, BooksDTO>();
            CreateMap<BooksDTO, Books>();

        }
    }
}
