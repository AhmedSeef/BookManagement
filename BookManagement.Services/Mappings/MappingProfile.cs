using AutoMapper;
using BookManagement.Data.Models;
using BookManagement.Services.DTOs;

namespace BookManagement.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
