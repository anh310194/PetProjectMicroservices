
using Identity.Core;
using Identity.Core.Entities.BaseEntity;

public class State : BaseEntity
{
    public required int CountryId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public EnumStatus Status { get; set; }

    public Country? Country { get; set; }
}