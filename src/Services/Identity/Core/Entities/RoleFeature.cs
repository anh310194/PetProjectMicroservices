using Core.Entities.BaseEntity;

public class RoleFeature : BaseEntity
{
    public required int RoleId { get; set; }
    public required int FeatureId { get; set; }
    public bool IsDeleted { get; set; }
}