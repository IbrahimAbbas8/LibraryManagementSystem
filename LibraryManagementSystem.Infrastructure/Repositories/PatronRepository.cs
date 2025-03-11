using AutoMapper;
using Ecom.Infrastructure.Repositories;
using LibraryManagementSystem.Core.Dtos;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Sharing;
using LibraryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class PatronRepository : GenericRepository<Patron>, IPatronRepository
    {
        private readonly LibraryManagementSystemDbContext context;
        private readonly IMapper mapper;

        public PatronRepository(LibraryManagementSystemDbContext context, IMapper mapper) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IList<GetPatronDto>> GetAllAsync(Params Params)
        {
            var query = await context.Patrons
                .Include(c => c.ContactInfo)
                .AsNoTracking()
                .ToListAsync();

            // Search
            if (!string.IsNullOrEmpty(Params.Search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(Params.Search)).ToList();
            }

            // sorting
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                query = Params.Sort switch
                {
                    "DateAsync" => query.OrderBy(p => p.Id).ToList(),
                    "DateeDesc" => query.OrderByDescending(p => p.Id).ToList(),
                    _ => query.OrderBy(p => p.Name).ToList(),
                };
            }

            Params.TotalItems = query.Count;

            // paging
            query = query.Skip((Params.PageSize) * (Params.PageNumber - 1)).Take(Params.PageSize).ToList();

            return mapper.Map<IList<GetPatronDto>>(query);
        }
    }
}
