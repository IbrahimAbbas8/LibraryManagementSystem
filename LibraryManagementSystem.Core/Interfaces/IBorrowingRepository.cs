using LibraryManagementSystem.Core.Dtos;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IBorrowingRepository
    {
        Task<IList<BorrowingRecord>> GetAllAsync();
        Task<BorrowingRecord> BorrowBook(int bookId, int patronId);
        Task<BorrowingRecord> ReturnBook(int bookId, int patronId);
    }
}
