namespace Identity.Service.Models
{
    public class RegisterUserModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Address { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public required string TenantName { get; set; }
    }
}
