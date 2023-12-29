using System;
using Quartz;
using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.Data;
using AperionQB.Infrastructure.QuickBooks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AperionQB.Infrastructure.QuartsJobs;
using static AperionQB.Infrastructure.QuartsJobs.UpdateAccessTokens;

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
                            .UseLazyLoadingProxies());
                            // The following three options help with debugging, but should
                            // be changed or removed for production.
                            //.LogTo(Console.WriteLine, LogLevel.Information)
                            //.EnableSensitiveDataLogging()
                            //.EnableDetailedErrors());

            services.AddSingleton<IQuickBooksManager, QuickBooksManager>();

            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();

            });

            //Wait for jobs to complete before shutting down
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });


            //Runs the setup for each job being scheduled
            services.ConfigureOptions<CheckDBForNewPaymentsSetup>();
            services.ConfigureOptions<UpdateAccessTokensSetup>();
            services.ConfigureOptions<CheckDBForPaymentUpdatesSetup>();
            services.ConfigureOptions<CheckDBForPaymentDeletionsSetup>();

            services.AddQuartzHostedService();

            
            return services;
        }
    }
}

