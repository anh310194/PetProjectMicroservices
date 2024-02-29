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
            entity.Property(k => k.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status).HasConversion<byte>();

            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.Property(k => k.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Status).HasConversion<byte>();


            entity.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}