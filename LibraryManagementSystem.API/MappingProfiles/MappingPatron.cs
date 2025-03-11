using AutoMapper;
using LibraryManagementSystem.Core.Dtos;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.API.MappingProfiles
{
    public class MappingPatron : Profile
    {
        public MappingPatron()
        {
            CreateMap<Patron, PatronDto>().ReverseMap();
            CreateMap<Patron, GetPatronDto>().ReverseMap();
            CreateMap<Patron, UpdatePatronDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
        }
    }
}
