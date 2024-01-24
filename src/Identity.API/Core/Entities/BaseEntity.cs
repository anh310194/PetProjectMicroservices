namespace Identity.API.Core.Entities.BaseEntity;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public long CreatedBy { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public long? UpdatedBy { get; set; }
    public byte[]? RowVersion { get; set; }
}