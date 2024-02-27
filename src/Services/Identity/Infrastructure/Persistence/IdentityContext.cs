using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class IdentityContext : DbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(k => k.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsUnicode().HasMaxLength(255);
            entity.Property(e => e.Status).HasConversion<byte>();

            entity.Property(e => e.CreatedTime).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedTime).ValueGeneratedOnUpdate();
            entity.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(k => k.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsUnicode().HasMaxLength(255);
            entity.Property(e => e.Status).HasConversion<byte>();
            entity.Property(e => e.CreatedTime).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedTime).ValueGeneratedOnUpdate();
            entity.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(k => k.Id);
            entity.Property(e => e.UserName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.SaltPassword).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(255).IsUnicode();
            entity.Property(e => e.StateId);
            entity.Property(e => e.CountryId);
            entity.Property(e => e.Status).HasConversion<byte>();

            entity.Property(e => e.CreatedTime).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedTime).ValueGeneratedOnUpdate();
            entity.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}