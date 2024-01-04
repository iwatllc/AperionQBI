using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.Logging;
using Quartz;
using Quartz.Impl;

namespace AperionQB.Infrastructure.QuartsJobs
{
    public class QuartsJobManager : IQuartsJobManager
    {
        IScheduler scheduler;
        JobKey deleteJobKey = JobKey.Create(nameof(CheckDBForPaymentDeletions));
        JobKey addJobKey = JobKey.Create(nameof(CheckDBForNewPayments));
        JobKey updateJobKey = JobKey.Create(nameof(CheckDBForPaymentUpdates));
        JobKey addCompanyJobKey = JobKey.Create(nameof(CheckDBForNewCustomers));
        Logger logger;

        public QuartsJobManager()
        {
            IReadOnlyList<IScheduler> schedulers = SchedulerRepository.Instance.LookupAll().Result;
            scheduler = schedulers.ElementAt(0);
            logger = new Logger();
        }

        public bool fireDeletePaymentsJob()
        {
            try
            {
                scheduler.TriggerJob(deleteJobKey);
                return true;
            }
            catch (Exception e)
            {
                logger.log(DateTime.Now + ": Unable to trigger new customers job: " + e.Message);
                return false;
            }
        }

        public bool fireNewPaymentsJob()
        {
            try
            {
                scheduler.TriggerJob(addJobKey);
                return true;

            }
            catch (Exception e)
            {
                logger.log(DateTime.Now + ": Unable to trigger new customers job: " + e.Message);
                return false;
            }
        }

        public bool fireUpdatePaymentsJob()
        {
            try
            {
                scheduler.TriggerJob(updateJobKey);
                return true;
            }
            catch (Exception e)
            {
                logger.log(DateTime.Now + ": Unable to trigger new customers job: " + e.Message);
                return false;
            }
        }

        public bool fireNewCustomersJob()
        {
            try
            {
                scheduler.TriggerJob(addCompanyJobKey);
                return true;
            }
            catch (Exception e)
            {
                logger.log(DateTime.Now + ": Unable to trigger new customers job: " + e.Message);
                return false;
            }
        }
    }
}