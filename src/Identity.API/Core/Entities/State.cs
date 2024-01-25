
using Identity.API.Core.Entities.BaseEntity;

public class State : BaseEntity
{
    public required int CountryId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public byte Status { get; set; }
}