using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.Logging;
using AperionQB.Infrastructure.QuickBooks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace AperionQB.Infrastructure.QuartsJobs
{
    public class UpdateAccessTokens : IJob
    {
        private readonly Logger _logger;
        private readonly IApplicationDbContext _context;
        private readonly IInfoHandler _handler;

        public UpdateAccessTokens(IInfoHandler _handler, IApplicationDbContext _context)
        {
            _logger = new Logger();
            this._context = _context;
            this._handler = _handler;
        }

        public Task Execute(IJobExecutionContext context)
        {

            try
            {
                _logger.log(DateTime.Now + ": About to update access tokens");
                QuickBooksKeyActions actions = new QuickBooksKeyActions(_context, _handler);
                bool result = actions.refreshAccessTokens().Result;
                if (result)
                {
                    _logger.log(DateTime.Now + ": Successfully updated access tokens");
                }
                else
                {
                    _logger.log(DateTime.Now + ": An error occured while updating access tokens");
                }
            }
            catch (Exception e)
            {
                _logger.log(DateTime.Now + ":" + e.Message);
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
                            schedule.WithIntervalInMinutes(15).RepeatForever()));
            }
        }
    }
}