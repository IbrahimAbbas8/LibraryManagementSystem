using AutoMapper;
using LibraryManagementSystem.Core.Dtos;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.API.MappingProfiles
{
    public class MappingBorrowingRecord : Profile
    {
        public MappingBorrowingRecord()
        {
            CreateMap<BorrowingRecord, BorrowingRecordDto>().ReverseMap();
        }
    }
}
