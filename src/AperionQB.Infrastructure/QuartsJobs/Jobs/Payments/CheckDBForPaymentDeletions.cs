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
    public class CheckDBForPaymentDeletions : IJob
    {
        private readonly Logger _logger;
        private readonly IApplicationDbContext _context;
        private readonly IInfoHandler _handler;
        public CheckDBForPaymentDeletions(IInfoHandler _handler, IApplicationDbContext _context)
        {
            _logger = new Logger();
            this._context = _context;
            this._handler = _handler;
        }


        public Task Execute(IJobExecutionContext context)
        {
            try
            {

                _logger.log(DateTime.Now + ": Checking for new payment deletions in bzb");
                int count = _context.PaymentsToMigrateToIntuit.ToList().Where((pmnt) => (pmnt.DeletedBool == true && pmnt.DeletedByQBIDate == null)).Count();
                if (count > 0)
                {
                    _logger.log(DateTime.Now + ": Found " + count + " payment deletions to process");
                    bool result = new DeleteAllFlaggedPayments(_context, _handler).deleteAllFlaggedPaymentsFromQuickBooks().Result;

                }
                else
                {
                    _logger.log(DateTime.Now + ": No new payment deletions to process");
                }

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.log(DateTime.Now + ": An error occured during job execution(CheckDBForPaymentDeletions): " + e.Message + "\n" + e.StackTrace);
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
                    .StartAt(DateTimeOffset.Now.AddMinutes(8))
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(2).RepeatForever()));
        }
    }
}


