using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class MasterDataContext : DbContext
{
    public MasterDataContext(DbContextOptions<MasterDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(k => k.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(100);
            entity.Property(e => e.Status).HasConversion<byte>();

            entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedTime).ValueGeneratedOnAdd().HasColumnType("TIMESTAMP"); ;
            entity.Property(e => e.UpdatedBy).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedTime).ValueGeneratedOnAddOrUpdate().HasColumnType("TIMESTAMP");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(k => k.Id);
            entity.Property(e => e.CountryId).IsRequired();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Status).HasConversion<byte>();

            entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedTime).ValueGeneratedOnAdd().HasColumnType("TIMESTAMP"); ;
            entity.Property(e => e.UpdatedBy).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedTime).ValueGeneratedOnAddOrUpdate().HasColumnType("TIMESTAMP");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}