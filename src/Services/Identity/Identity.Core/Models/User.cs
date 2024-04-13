using Utilities;

namespace Identity.Core.Models
{
    public class User : BaseEntity
    {
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Password { get; set; }
        public string? SaltPassword { get; set; }
        public string? Address { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public int RoleId { get; set; }
        public EnumStatus Status { get; set; }
        public bool IsSystemAdmin { get; set; }
        public string? AvatarUrl { get; set; }

        public Role Role { get; set; } = null!;
    }
}
