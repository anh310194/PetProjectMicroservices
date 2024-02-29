using System.ComponentModel.DataAnnotations;

namespace Core.Entities.BaseEntity;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public int? UpdatedBy { get; set; }

    public byte[] RowVersion { get; set; } = new byte[0];
}