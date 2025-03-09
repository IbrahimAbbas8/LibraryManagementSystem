using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Data
{
    public class LibraryManagementSystemDbContext : DbContext
    {
        public LibraryManagementSystemDbContext(DbContextOptions<LibraryManagementSystemDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<BorrowingRecord> BorrowingRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
