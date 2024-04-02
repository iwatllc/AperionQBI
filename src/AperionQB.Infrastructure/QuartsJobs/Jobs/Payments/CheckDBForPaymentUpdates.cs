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
    public class CheckDBForPaymentUpdates : IJob
    {
        private readonly Logger _logger;
        private readonly IApplicationDbContext _context;
        private readonly IInfoHandler _handler;
        public CheckDBForPaymentUpdates(IInfoHandler _handler, IApplicationDbContext _context)
        {
            _logger = new Logger();
            this._context = _context;
            this._handler = _handler;
        }


        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.log(DateTime.Now + ": Checking for payment updates in bzb");
                int count = _context.QBUpdateTransactions.ToList().Where((trans) => (trans.updateBool == false && trans.datePosted == null)).Count();

                if (count > 0)
                {
                    _logger.log(DateTime.Now + ": Found " + count + " new payment updates to process");
                    bool result =  new UpdateAllPaymentsInQuickBooks(_context, _handler).UpdateAllPaymentsInQB().Result;
                }
                else
                {
                    _logger.log(DateTime.Now + ": No new payment updates to process");
                }

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.log(DateTime.Now + ": An error occured during job execution(CheckDBForPaymentUpdates): " + e.Message + "\n" + e.StackTrace);
                return Task.CompletedTask;
            }

        }
    }

    public class CheckDBForPaymentUpdatesSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            JobKey jobKey = JobKey.Create(nameof(CheckDBForPaymentUpdates));
            options
                .AddJob<CheckDBForPaymentUpdates>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(trigger => trigger
                    .ForJob(jobKey)
                    .StartAt(DateTimeOffset.Now.AddMinutes(7))
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(2).RepeatForever()));
        }
    }
}

