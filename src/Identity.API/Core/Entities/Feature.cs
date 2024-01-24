
using Identity.API.Core.Entities.BaseEntity;

public class Feature : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public byte Status { get; set; }
}