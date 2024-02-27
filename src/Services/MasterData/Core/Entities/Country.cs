using Core;
using Core.Entities.BaseEntity;

public class Country : BaseEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public EnumStatus Status { get; set; }

    public ICollection<State>? States { get; set; }
}