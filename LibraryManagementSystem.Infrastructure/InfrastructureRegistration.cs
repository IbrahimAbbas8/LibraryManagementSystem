using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Repositories;
using LibraryManagementSystem.Core.Events;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Infrastructure.Data;
using LibraryManagementSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IDomainEventPublisher, DomainEventPublisher>();
            services.AddSingleton<IBookAddedObserver, BookAddedNotificationObserver>();

            services.AddDbContext<LibraryManagementSystemDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefulteConnection"));
            });

            return services;
        }
    }
}
