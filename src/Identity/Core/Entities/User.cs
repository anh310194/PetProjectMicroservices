using Identity.Core;
using Identity.Core.Entities.BaseEntity;

public class User : BaseEntity
{
    public required string UserName { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Password { get; set; }
    public string? SaltPassword { get; set; }
    public string? Address { get; set; }
    public EnumStatus Status { get; set; }
    public int? CountryId { get; set; }
    public int? StateId { get; set; }

    public Country? Country { get; set; }
    public State? State { get; set; }
}