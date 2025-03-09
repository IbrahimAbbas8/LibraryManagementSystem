using Ecom.Core.Interfaces;
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
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IList<BookDto>> GetAllAsync(Params Params);

        Task<bool> AddAsync(BookDto entity);
    }
}
