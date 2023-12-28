
namespace AperionQB.Domain.Entities;

public partial class ContactAddress
{
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public int AddressTypeId { get; set; }
    public bool Billing { get; set; }
    public string? City { get; set; }
    public virtual Contact Contact { get; set; } = null!;
    public int ContactId { get; set; }
    public DateTime Created { get; set; }
    public int Id { get; set; }
    public DateTime? Modified { get; set; }
    public bool PrimaryAddress { get; set; }
    public string? State { get; set; }
    public int StatusId { get; set; }
    public string? Zip { get; set; }

    public string? Zipplus { get; set; }
}
