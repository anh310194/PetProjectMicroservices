namespace Identity.Core.Models
{
    public class Tenant : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
