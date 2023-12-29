using System;
using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.BZBQB;
using AperionQB.Infrastructure.QuickBooks.Payments;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
namespace AperionQB.Infrastructure
{
    //Not allowed to run more than one of this job type at a time
    [DisallowConcurrentExecution]
    public class CheckDBForPaymentDeletions : IJob
    {
        private readonly ILogger<CheckDBForPaymentDeletions> _logger;
        private readonly IApplicationDbContext _context;
        public CheckDBForPaymentDeletions(ILogger<CheckDBForPaymentDeletions> logger, IApplicationDbContext _context)
        {
            _logger = logger;
            this._context = _context;
        }


        public Task Execute(IJobExecutionContext context)
        {
            try
            {

                Console.WriteLine(DateTime.Now + ": Checking for new payment deletions in bzb");
                int count = _context.PaymentsToMigrateToIntuit.ToList().Where((pmnt) => (pmnt.DeletedBool == true)).Count();
                if (count > 0)
                {
                    Console.WriteLine(DateTime.Now + ": Found " + count + " payment deletions to process");
                    bool result = new DeleteAllFlaggedPayments(_context).deleteAllFlaggedPaymentsFromQuickBooks().Result;

                }
                else
                {
                    Console.WriteLine(DateTime.Now + ": No new payment deletions to process");
                }

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now + ": An error occured during job execution(CheckDBForPaymentDeletions): " + e.Message + "\n" + e.StackTrace);
                return Task.CompletedTask;
            }

        }
    }

    public class CheckDBForPaymentDeletionsSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            JobKey jobKey = JobKey.Create(nameof(CheckDBForPaymentDeletions));
            options
                .AddJob<CheckDBForPaymentDeletions>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(trigger => trigger
                    .ForJob(jobKey)
                    .StartAt(DateTimeOffset.Now.AddMinutes(5))
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(30).RepeatForever()));
        }
    }
}


