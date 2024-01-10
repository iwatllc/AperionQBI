using System;
using Quartz;
using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.Data;
using AperionQB.Infrastructure.QuickBooks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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


            services.AddScoped<IQuickBooksManager, QuickBooksManager>();
            services.AddSingleton<IQuartsJobManager, QuartsJobManager>();
            services.AddScoped<IInfoHandler, IntuitInfoHandler>();

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
            services.ConfigureOptions<UpdateAccessTokensSetup>();
            services.ConfigureOptions<CheckDBForNewPaymentsSetup>();
            services.ConfigureOptions<CheckDBForPaymentUpdatesSetup>();
            services.ConfigureOptions<CheckDBForPaymentDeletionsSetup>();
            services.ConfigureOptions<CheckDBForNewCustomersSetup>();


            services.AddQuartzHostedService();


            return services;
        }
    }
}