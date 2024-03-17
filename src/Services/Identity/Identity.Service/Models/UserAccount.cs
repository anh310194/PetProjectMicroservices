namespace Identity.Service.Models
{
    public class UserAccount
    {
        public required string UserName { get; set; }
        public required string DisplayName { get; set; }
        public required string Role { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }

    }
}
