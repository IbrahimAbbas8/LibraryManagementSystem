using AutoMapper;
using Ecom.Infrastructure.Repositories;
using LibraryManagementSystem.Core.Dtos;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Events;
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
    internal class BookRepository : GenericRepository<Book> , IBookRepository
    {
        private readonly LibraryManagementSystemDbContext context;
        private readonly IMapper mapper;
        private readonly IDomainEventPublisher domainEventPublisher;

        public BookRepository(LibraryManagementSystemDbContext context, IMapper mapper, IDomainEventPublisher domainEventPublisher) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.domainEventPublisher = domainEventPublisher;
        }

        public async Task<IList<GetBookDto>> GetAllAsync(Params Params)
        {
            var query = await context.Books
                .Include(c => c.BorrowingRecords)
                .AsNoTracking()
                .ToListAsync();

            // Search
            if (!string.IsNullOrEmpty(Params.Search))
            {
                query = query.Where(p => p.Title.ToLower().Contains(Params.Search)).ToList();
            }

            // sorting
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                query = Params.Sort switch
                {
                    "DateAsync" => query.OrderBy(p => p.PublicationYear).ToList(),
                    "DateeDesc" => query.OrderByDescending(p => p.PublicationYear).ToList(),
                    _ => query.OrderBy(p => p.Title).ToList(),
                };
            }

            Params.TotalItems = query.Count;

            // paging
            query = query.Skip((Params.PageSize) * (Params.PageNumber - 1)).Take(Params.PageSize).ToList();

            return mapper.Map<IList<GetBookDto>>(query);
        }

        public async Task<bool> AddAsync(BookDto  dto)
        {
            var entity = mapper.Map<Book>(dto);
            await context.AddAsync(entity);
            domainEventPublisher.PublishBookAdded(new BookAddedEvent(entity));
            await context.SaveChangesAsync();
            return true;
        }
    }
}
