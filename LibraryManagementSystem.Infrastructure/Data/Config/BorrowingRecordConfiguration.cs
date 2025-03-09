using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LibraryManagementSystem.Infrastructure.Data.Config
{
    public class BorrowingRecordConfiguration : IEntityTypeConfiguration<BorrowingRecord>
    {
        public void Configure(EntityTypeBuilder<BorrowingRecord> builder)
        {
            builder.Property(br => br.BorrowDate).IsRequired();
            builder.Property(br => br.ReturnDate);

            builder.HasData(
                new BorrowingRecord
                {
                    Id = 1,
                    BookId = 1,  
                    PatronId = 1,
                    BorrowDate = new DateTime(2023, 1, 15),
                    ReturnDate = new DateTime(2023, 1, 25)
                },
                new BorrowingRecord
                {
                    Id = 2,
                    BookId = 2,  
                    PatronId = 2,
                    BorrowDate = new DateTime(2023, 2, 5),
                    ReturnDate = null
                }
            );
        }
    }
}
