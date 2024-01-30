
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Infrastructures;

public partial class IdentityContext : DbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Code).IsRequired().HasMaxLength(10);

            entity.Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(100);

            entity.Property(e => e.Description).IsUnicode().HasMaxLength(255);

            entity.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

            entity.Property(e => e.Description).IsUnicode().HasMaxLength(255);

            entity.Property(e => e.Status).HasConversion<byte>();

            entity.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

            entity.Property(e => e.Description).IsUnicode().HasMaxLength(255);

            entity.Property(e => e.Status).HasConversion<byte>();

            entity.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.Property(e => e.CountryId).IsRequired();

            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

            entity.Property(e => e.Description).IsUnicode().HasMaxLength(255);

            entity.Property(e => e.Status).HasConversion<byte>();

            entity.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserName).HasMaxLength(100).IsRequired();

            entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();

            entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();

            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();

            entity.Property(e => e.Password).HasMaxLength(10);

            entity.Property(e => e.SaltPassword).HasMaxLength(50);

            entity.Property(e => e.Address).HasMaxLength(255).IsUnicode();

            entity.Property(e => e.StateId);

            entity.Property(e => e.CountryId);

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