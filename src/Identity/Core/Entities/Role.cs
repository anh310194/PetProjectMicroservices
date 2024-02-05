
using Identity.Core;
using Identity.Core.Entities.BaseEntity;

public class Role : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public EnumStatus Status { get; set; }
}