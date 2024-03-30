namespace Identity.Core.Models
{
    public class RoleFeature : BaseEntity
    {
        public int RoleId { get; set; }
        public int FeatureId { get; set; }
        public bool IsDeleted { get; set; }
        public Role Role { get; set; } = null!;
        public Feature Feature { get; set; } = null!;
    }
}
