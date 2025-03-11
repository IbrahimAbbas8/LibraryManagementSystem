using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly LibraryManagementSystemDbContext context;

        public BorrowingRepository(LibraryManagementSystemDbContext context)
        {
            this.context = context;
        }

        public async Task<BorrowingRecord> BorrowBook(int bookId, int patronId)
        {
            // Verify that the book and patron exist
            var book = await context.Books.FindAsync(bookId);
            var patron = await context.Patrons.FindAsync(patronId);
            if (book == null || patron == null)
                throw new Exception("Book or Patron not found");
            if(book.IsBorrowed)
                throw new Exception("This book is not available for loan");

            book.IsBorrowed = true;

            // Check if the book is already borrowed (active borrowing record exists)
            var existingRecord = await context.BorrowingRecords
                .FirstOrDefaultAsync(r => r.BookId == bookId && r.ReturnDate == null);
            if (existingRecord != null)
                throw new Exception("The book is already borrowed");

            var record = new BorrowingRecord
            {
                BookId = bookId,
                PatronId = patronId,
                BorrowDate = DateTime.Now
            };

            context.BorrowingRecords.Add(record);
            await context.SaveChangesAsync();

            return record;
        }

        public async Task<IList<BorrowingRecord>> GetAllAsync()
        {
            return await context.BorrowingRecords.ToListAsync();
        }

        public async Task<BorrowingRecord> ReturnBook(int bookId, int patronId)
        {
            // Find the active borrowing record for the specified book and patron
            var book = await context.Books.FindAsync(bookId);
            var patron = await context.Patrons.FindAsync(patronId);
            if (book == null || patron == null)
                throw new Exception("Book or Patron not found");
            book.IsBorrowed = false;

            var record = await context.BorrowingRecords
                .FirstOrDefaultAsync(r => r.BookId == bookId && r.PatronId == patronId && r.ReturnDate == null);
            if (record == null)
                throw new Exception("Borrowing record not found or the book has already been returned");

            record.ReturnDate = DateTime.Now;
            await context.SaveChangesAsync();

            return record;
        }
    }
}
