namespace AperionQB.Application.Interfaces
{
    public interface IQuartsJobManager
    {
        bool fireNewPaymentsJob();
        bool fireUpdatePaymentsJob();
        bool fireDeletePaymentsJob();
        bool fireNewCustomersJob();
    }
}