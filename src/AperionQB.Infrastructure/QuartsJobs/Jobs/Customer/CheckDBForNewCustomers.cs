using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.Logging;
using AperionQB.Infrastructure.QuickBooks.Payments;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
namespace AperionQB.Infrastructure
{
    //Not allowed to run more than one of this job type at a time
    [DisallowConcurrentExecution]
    public class CheckDBForNewCustomers : IJob
    {
        private readonly Logger _logger;
        private readonly IApplicationDbContext _context;
        private readonly IInfoHandler _handler;
        public CheckDBForNewCustomers(IInfoHandler _handler, IApplicationDbContext _context)
        {
            _logger = new Logger();
            this._context = _context;
            this._handler = _handler;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.log(DateTime.Now + ": Checking for new companies in bzb");
                int count = _context.BZBQuickBooksCustomerMappings.ToList().Where((map) => (map.qbId == -1)).Count();
                if (count > 0)
                {
                    _logger.log(DateTime.Now + ": Found " + count + " new companies to process");
                    bool result = await new AddAllCustomerToQuickBooks(_context, _handler).addCustomerstoQuickBooks();

                }
                else
                {
                    _logger.log(DateTime.Now + ": No new companies to process");
                }

                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.log(DateTime.Now + ": An error occured during job execution(CheckDBForNewCustomers): " + e.Message);
                await Task.CompletedTask;
            }

        }
    }

    public class CheckDBForNewCustomersSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            JobKey jobKey = JobKey.Create(nameof(CheckDBForNewCustomers));
            options
                .AddJob<CheckDBForNewCustomers>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(trigger => trigger
                    .ForJob(jobKey)
                    .StartAt(DateTimeOffset.Now.AddMinutes(15))
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(30).RepeatForever()));
        }
    }
}

