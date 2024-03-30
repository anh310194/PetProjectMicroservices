using Utilities;

namespace MasterData.Domain.Entities;

public class Country : BaseEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public EnumStatus Status { get; set; }

    public ICollection<State> States { get; } = new List<State>();
}