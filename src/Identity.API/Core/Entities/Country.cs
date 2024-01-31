
using Identity.API.Core.Entities.BaseEntity;

public class Country : BaseEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<State>? States { get; set; }
}