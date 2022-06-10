namespace Blocks.Shared.ValueObjects;

public record Address
{
    public Guid AddressID { get; set; }
    public string? AddressLine1 { get; init; }
    public string? AddressLine2 { get; init; }
    public string? Street { get; init; }
    public string? Town { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
    public string? PhoneNumber { get; set; }
    public string? EmailAdress { get; set; }
}