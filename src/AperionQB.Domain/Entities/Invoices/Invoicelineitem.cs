

namespace AperionQB.Domain.Entities;

public partial class InvoiceLineItem
{
    public decimal Amount { get; set; }


    public virtual Companynote? CompanyNote { get; set; }
    public int? CompanyNoteId { get; set; }
    public virtual ICollection<Companynote> CompanyNotes { get; set; } = new List<Companynote>();
    public virtual ContactNote? ContactNote { get; set; }
    public int? ContactNoteId { get; set; }
    public virtual ICollection<ContactNote> ContactNotes { get; set; } = new List<ContactNote>();
    public virtual InvoiceCustomItem? CustomInvoice { get; set; }
    public int? CustomInvoiceId { get; set; }
    public int Id { get; set; }

    public bool Ignored { get; set; }
    public virtual Invoice Invoice { get; set; } = null!;
    public int InvoiceId { get; set; }
    public int? ProjectTaskActionId { get; set; }
    public bool? Taxable { get; set; }
    public int? TicketActionId { get; set; }

    public int? TicketChargeId { get; set; }
    public int? TicketPartId { get; set; }
    public int? ToDoActionId { get; set; }
}
