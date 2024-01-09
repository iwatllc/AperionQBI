using System;
using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.BZBQB;
using AperionQB.Infrastructure.Logging;
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
        private readonly Logger _logger;
        private readonly IApplicationDbContext _context;
        private readonly IInfoHandler _handler;
        public CheckDBForNewPayments(IInfoHandler _handler, IApplicationDbContext _context)
        {
            _logger = new Logger();
            this._context = _context;
            this._handler = _handler;
        }


        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.log(DateTime.Now + ": Checking for new payments in bzb");
                int count = _context.PaymentsToMigrateToIntuit.ToList().Where((pmnt) => (pmnt.intuitPaymentID == null && pmnt.DeletedBool == false)).Count();
                if (count > 0)
                {
                    _logger.log(DateTime.Now + ": Found " + count + " new payments to process");
                    bool result = new AddAllPaymentsToQuickBooks(_context, _handler).addAllPaymentstoQuickBooks().Result;

                }
                else
                {
                    _logger.log(DateTime.Now + ": No new payments to process");
                }

                return Task.CompletedTask;
            }catch (Exception e)
            {
                _logger.log(DateTime.Now + ": An error occured during job execution(CheckDBForNewPayments): " + e.Message + "\n" + e.StackTrace);
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
                    .StartAt(DateTimeOffset.Now.AddMinutes(6))
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(5).RepeatForever()));
        }
    }
}

