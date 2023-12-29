using System;
using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.QuickBooks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace AperionQB.Infrastructure.QuartsJobs
{
	public class UpdateAccessTokens : IJob
	{
        private readonly ILogger<UpdateAccessTokens> _logger;
        private readonly IApplicationDbContext _context;

        public UpdateAccessTokens(ILogger<UpdateAccessTokens> logger, IApplicationDbContext _context)
        {
            _logger = logger;
            this._context = _context;
		}

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine(DateTime.Now + ": About to update access tokens");
                QuickBooksKeyActions actions = new QuickBooksKeyActions();
                bool result = actions.refreshAccessTokens().Result;
                if (result)
                {
                    Console.WriteLine(DateTime.Now + ": Successfully updated access tokens");
                }
                else
                {
                    Console.WriteLine(DateTime.Now + ": An error occured while updating access tokens");
                }
            }catch (Exception e)
            {
                Console.WriteLine(DateTime.Now + e.Message);
            }
            return Task.CompletedTask;

        }

        public class UpdateAccessTokensSetup : IConfigureOptions<QuartzOptions>
        {
            public void Configure(QuartzOptions options)
            {
                JobKey jobKey = JobKey.Create(nameof(UpdateAccessTokens));
                options
                    .AddJob<UpdateAccessTokens>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                    .AddTrigger(trigger => trigger
                        .ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule.WithIntervalInMinutes(30).RepeatForever()));
            }
        }
    }
}

