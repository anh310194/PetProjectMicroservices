using MasterData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MasterData.Infrastructure.Persistence;

public partial class MasterDataContext : DbContext
{
    public MasterDataContext(DbContextOptions<MasterDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Countries");
            entity.HasKey(k => k.Id);
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status).HasConversion<byte>();

            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("States");
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