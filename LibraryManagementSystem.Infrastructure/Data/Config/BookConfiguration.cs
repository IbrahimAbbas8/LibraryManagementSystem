using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LibraryManagementSystem.Infrastructure.Data.Config
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(60);
            builder.Property(x => x.Author).HasMaxLength(30);
            builder.Property(x => x.ISBN).HasMaxLength(60);
            builder.Property(x => x.PublicationYear)
                   .HasColumnType("date");

            builder.HasData(
                new Book
                {
                    Id = 1,
                    Title = "1984",
                    Author = "George Orwell",
                    PublicationYear = new DateTime(1949, 6, 8),
                    ISBN = "9780451524935"
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    PublicationYear = new DateTime(1960, 7, 11),
                    ISBN = "9780060935467"
                },
                new Book
                {
                    Id = 3,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    PublicationYear = new DateTime(1925, 4, 10),
                    ISBN = "9780743273565"
                }
            );
        }
    }
}
