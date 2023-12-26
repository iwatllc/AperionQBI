using System;
using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.Data;
using AperionQB.Infrastructure.QuickBooks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AperionQB.Infrastructure
{
	public static class DependencyInjection
	{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Setting up data/DB
            string connStr = configuration.GetConnectionString("DefaultConnection") ?? "";

            services.AddDbContext<IApplicationDbContext, BzbDbContext>(dbContextOptions => dbContextOptions
                            .UseMySql(connStr, ServerVersion.Parse("8.1.0-mysql"))
                            .UseLazyLoadingProxies()
                            // The following three options help with debugging, but should
                            // be changed or removed for production.
                            .LogTo(Console.WriteLine, LogLevel.Information)
                            .EnableSensitiveDataLogging()
                            .EnableDetailedErrors());

            services.AddSingleton<IQuickBooksManager, QuickBooksManager>();

            
            return services;
        }
    }
}

