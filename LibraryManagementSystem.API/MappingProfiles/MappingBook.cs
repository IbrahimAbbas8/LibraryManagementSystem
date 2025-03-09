using AutoMapper;
using LibraryManagementSystem.Core.Dtos;
using LibraryManagementSystem.Core.Entities;
using System.Net;

namespace LibraryManagementSystem.API.MappingProfiles
{
    public class MappingBook : Profile
    {
        public MappingBook()
        {
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
