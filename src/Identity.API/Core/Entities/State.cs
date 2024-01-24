
using Identity.API.Core.Entities.BaseEntity;

public class State : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public byte Status { get; set; }
}