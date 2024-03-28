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
	public class CheckDBForNewMassInvoicePayments : IJob
	{
        private readonly Logger _logger;
        private readonly IApplicationDbContext _context;
        private readonly IInfoHandler _handler;
        public CheckDBForNewMassInvoicePayments(IInfoHandler _handler, IApplicationDbContext _context)
        {
            _logger = new Logger();
            this._context = _context;
            this._handler = _handler;
        }


        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.log(DateTime.Now + ": Checking for new mass invoice payments in bzb");
                int count = _context.QBMassInvoicePayments.ToList().Where((pmnt) => (pmnt.intuitPaymentID == null && pmnt.DeletedBool == false)).Count();
                if (count > 0)
                {
                    _logger.log(DateTime.Now + ": Found " + count + " new payments to process");
                    bool result = new AddAllMassInvoicePaymentsToQuickBooks(_context, _handler).addAllMassInvoicePaymentstoQuickBooks().Result;

                }
                else
                {
                    _logger.log(DateTime.Now + ": No new mass invoice payments to process");
                }

                return Task.CompletedTask;
            }catch (Exception e)
            {
                _logger.log(DateTime.Now + ": An error occured during job execution(CheckDBForNewMassInvoicePayments): " + e.Message + "\n" + e.StackTrace);
                return Task.CompletedTask;
            }

        }
    }

    public class CheckDBForNewMassInvoicePaymentsSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            JobKey jobKey = JobKey.Create(nameof(CheckDBForNewMassInvoicePayments));
            options
                .AddJob<CheckDBForNewMassInvoicePayments>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(trigger => trigger
                    .ForJob(jobKey)
                    .StartAt(DateTimeOffset.Now.AddMinutes(5))
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(2).RepeatForever()));
        }
    }
}

