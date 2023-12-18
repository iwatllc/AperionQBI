namespace AperionQB.Domain.Entities;

public partial class InvoiceLog
{
    public DateTime DateSent { get; set; }
    public string EmailCC { get; set; } = null!;
    public string EmailFrom { get; set; } = null!;
    public string EmailMessage { get; set; } = null!;
    public string EmailSubject { get; set; } = null!;
    public string EmailTo { get; set; } = null!;
    public int Id { get; set; }
    public string InvoiceIds { get; set; } = null!;
}
