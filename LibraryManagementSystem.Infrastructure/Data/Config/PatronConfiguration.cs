using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Data.Config
{
    public class PatronConfiguration : IEntityTypeConfiguration<Patron>
    {
        public void Configure(EntityTypeBuilder<Patron> builder)
        {
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.OwnsOne(p => p.ContactInfo, ci =>
            {
                ci.Property(c => c.Email)
                  .IsRequired()
                  .HasMaxLength(100);

                ci.Property(c => c.Phone)
                  .IsRequired()
                  .HasMaxLength(20);

                ci.Property(c => c.Address)
                  .IsRequired()
                  .HasMaxLength(200);
            });

            builder.HasData(
                new Patron { Id = 1, Name = "John Doe" },
                new Patron { Id = 2, Name = "Jane Smith" }
            );

            builder.OwnsOne(p => p.ContactInfo).HasData(
                new
                {
                    PatronId = 1,
                    Email = "john.doe@example.com",
                    Phone = "111-222-3333",
                    Address = "123 Main St"
                },
                new
                {
                    PatronId = 2,
                    Email = "jane.smith@example.com",
                    Phone = "444-555-6666",
                    Address = "456 Oak Ave"
                }
            );
        }
    }
}
