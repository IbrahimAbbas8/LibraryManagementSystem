using AutoMapper;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryManagementSystemDbContext context;
        private readonly IMapper mapper;
        private readonly IDomainEventPublisher domainEventPublisher;

        public IBookRepository BookRepository { get; }

        public IPatronRepository PatronRepository { get; }

        public IBorrowingRepository BorrowingRepository { get; }

        public UnitOfWork(LibraryManagementSystemDbContext context, IMapper mapper, IDomainEventPublisher domainEventPublisher)
        {
            this.context = context;
            this.mapper = mapper;
            this.domainEventPublisher = domainEventPublisher;
            BookRepository = new BookRepository(context, mapper, domainEventPublisher);
            PatronRepository = new PatronRepository(context, mapper);
            BorrowingRepository = new BorrowingRepository(context);
        }
    }
}
