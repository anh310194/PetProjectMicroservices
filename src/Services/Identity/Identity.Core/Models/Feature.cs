using Utilities;

namespace Identity.Core.Models
{
    public class Feature : BaseEntity
    {
        public required string Code { get; set; }
        public string? Description { get; set; }
        public EnumStatus Status { get; set; }
        public ICollection<RoleFeature> RoleFeatures { get; set; } = new List<RoleFeature>();
    }
}
