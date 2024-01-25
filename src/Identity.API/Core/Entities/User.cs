
using Identity.API.Core.Entities.BaseEntity;

public class User : BaseEntity
{
    public required string UserName { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Password { get; set; }
    public string? SaltPassword { get; set; }
    public string? Address { get; set; }
    public byte Status { get; set; }
    public int? CountryId { get; set; }
    public int? StateId { get; set; }
}