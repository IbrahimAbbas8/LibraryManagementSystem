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
    public interface IPatronRepository : IGenericRepository<Patron>
    {
        Task<IList<GetPatronDto>> GetAllAsync(Params Params);
    }
}
