
namespace AperionQB.Domain.Entities;

public partial class CompanyCommunication
{
    public int CommunicationSubTypeId { get; set; }
    public int CommunicationTypeId { get; set; }
    public virtual Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
    public DateTime Created { get; set; }
    public int Id { get; set; }
    public DateTime Modified { get; set; }
    public int PrimaryCommunication { get; set; }
    public int StatusId { get; set; }
    public string Value { get; set; } = null!;
}
