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
	public class CheckDBForNewPayments : IJob
	{
        private readonly ILogger<CheckDBForNewPayments> _logger;
        private readonly IApplicationDbContext _context;
        public CheckDBForNewPayments(ILogger<CheckDBForNewPayments> logger, IApplicationDbContext _context)
        {
            _logger = logger;
            this._context = _context;
        }


        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine(DateTime.UtcNow + ": Checking for new payments in bzb");
                int count = _context.PaymentsToMigrateToIntuit.ToList().Where((pmnt) => (pmnt.intuitPaymentID == null)).Count();
                if (count > 0)
                {
                    Console.WriteLine(DateTime.UtcNow + ": Found " + count + " new payments to process");
                    bool result = new AddAllPaymentsToQuickBooks(_context).addAllPaymentstoQuickBooks();

                }
                else
                {
                    Console.WriteLine(DateTime.UtcNow + ": No new payments to process");
                }

                return Task.CompletedTask;
            }catch (Exception e)
            {
                Console.WriteLine(DateTime.UtcNow + ": An error occured during job execution(CheckDBForNewPayments): " + e.Message + "\n" + e.StackTrace);
                return Task.CompletedTask;
            }

        }
    }

    public class CheckDBForNewPaymentsSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            JobKey jobKey = JobKey.Create(nameof(CheckDBForNewPayments));
            options
                .AddJob<CheckDBForNewPayments>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(trigger => trigger
                    .ForJob(jobKey)
                    //.StartAt(new DateTimeOffset().AddSeconds(10))
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(5).RepeatForever()));
        }
    }
}

