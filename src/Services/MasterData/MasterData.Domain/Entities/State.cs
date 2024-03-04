namespace MasterData.Domain.Entities;

public class State : BaseEntity
{
    public required int CountryId { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public EnumStatus Status { get; set; }

    public Country? Country { get; set; }
}