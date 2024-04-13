﻿using Identity.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence;

public partial class IdentityContext : DbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<Feature> Features { get; set; }
    DbSet<RoleFeature> RoleFeatures { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(k => k.Id);
            entity.Property(e => e.UserName).HasMaxLength(20);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(220);
            entity.Property(e => e.Status).HasConversion<byte>();
            entity.Property(e => e.AvatarUrl).HasMaxLength(100);
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(e => e.Role).WithMany(e => e.Users).HasForeignKey(e => e.RoleId).IsRequired();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles");
            entity.HasKey(k => k.Id);
            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(220);
            entity.Property(e => e.Status).HasConversion<byte>();
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasMany(e => e.Users).WithOne(e => e.Role).HasForeignKey(e => e.RoleId).IsRequired();
            entity.HasMany(e => e.RoleFeatures).WithOne(e => e.Role).HasForeignKey(e => e.RoleId).IsRequired();
        });

        modelBuilder.Entity<RoleFeature>(entity =>
        {
            entity.ToTable("RoleFeatures");
            entity.HasKey(k => k.Id);
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(e => e.Role).WithMany(e => e.RoleFeatures).HasForeignKey(e => e.RoleId).IsRequired();
            entity.HasOne(e => e.Feature).WithMany(e => e.RoleFeatures).HasForeignKey(e => e.FeatureId).IsRequired();
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.ToTable("Features");
            entity.HasKey(k => k.Id);
            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(220);
            entity.Property(e => e.Status).HasConversion<byte>();
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasMany(e => e.RoleFeatures).WithOne(e => e.Feature).HasForeignKey(e => e.RoleId).IsRequired();
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

