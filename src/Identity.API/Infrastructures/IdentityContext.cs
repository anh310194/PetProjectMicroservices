
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

            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}