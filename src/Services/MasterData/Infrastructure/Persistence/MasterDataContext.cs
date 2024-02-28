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

            entity.Property(e => e.CreatedBy);
            entity.Property(e => e.CreatedTime);
            entity.Property(e => e.UpdatedBy);
            entity.Property(e => e.UpdatedTime);
            entity.Property(e => e.RowVersion)
            //.IsRowVersion()
            .IsRequired()
            ; // Configure as row version column
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
            entity.Property(e => e.UpdatedBy).ValueGeneratedOnUpdate();
            entity.Property(e => e.UpdatedTime).ValueGeneratedOnUpdate().HasColumnType("TIMESTAMP");
            entity.Property(e => e.RowVersion)
                .HasColumnType("binary(16)") // Map to BINARY(16) in MySQL
                .IsRowVersion(); // Configure as row version column
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}