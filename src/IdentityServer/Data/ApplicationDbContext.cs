﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Models;

namespace IdentityServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual new DbSet<Role> Roles { get; set; }
        public virtual new DbSet<User> Users { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }

        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        */


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName).HasName("RoleNameIndex");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.HasMany(d => d.RolePermissions).WithOne(p => p.Role).HasForeignKey(d => d.RoleId);

                entity.ToTable("Roles");
            });

            builder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail).HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName).HasName("UserNameIndex");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.UserPermissions).WithOne(p => p.User).HasForeignKey(d => d.UserId);
                entity.ToTable("Users");
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.ToTable("UserRoles");
            });


            builder.Entity<Permission>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("UK_Permission").IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Group)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.ToTable("Permissions");
            });





            builder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionId });

                entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions).HasForeignKey(d => d.PermissionId);

                entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions).HasForeignKey(d => d.RoleId);
                entity.ToTable("RolePermissions");
            });

            builder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PermissionId });

                entity.HasOne(d => d.Permission).WithMany(p => p.UserPermissions).HasForeignKey(d => d.PermissionId);

                entity.HasOne(d => d.User).WithMany(p => p.UserPermissions).HasForeignKey(d => d.UserId);
                entity.ToTable("UserPermissions");
            });
        }
    }
}
