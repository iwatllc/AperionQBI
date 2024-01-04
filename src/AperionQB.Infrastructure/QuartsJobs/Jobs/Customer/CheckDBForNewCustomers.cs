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
        public CheckDBForNewCustomers(ILogger<CheckDBForNewPayments> logger, IApplicationDbContext _context)
        {
            _logger = new Logger();
            this._context = _context;
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
                    bool result = await new AddAllCustomerToQuickBooks(_context).addCustomerstoQuickBooks();

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
                    .StartAt(DateTimeOffset.Now.AddMinutes(10))
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(30).RepeatForever()));
        }
    }
}

