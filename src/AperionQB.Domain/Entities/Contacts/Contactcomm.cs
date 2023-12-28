namespace AperionQB.Domain.Entities;

public partial class ContactCommunication
{
    public int? CommunicationSubTypeId { get; set; }
    public int CommunicationTypeId { get; set; }
    public virtual Contact Contact { get; set; } = null!;
    public int ContactId { get; set; }
    public DateTime Created { get; set; }
    public bool GoogleCommunication { get; set; }
    public int Id { get; set; }
    public DateTime? Modified { get; set; }
    public bool PrimaryCommunication { get; set; }
    public int StatusId { get; set; }
    public string? Value { get; set; }
}
