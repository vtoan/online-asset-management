using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Utils
{
    public class ModelBuilderHelper
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");

            modelBuilder.Entity<UserExtension>(entity =>
            {
                entity.ToTable("UserExtension");

                entity.Property(e => e.UserName)
                    .IsUnicode(true)
                    .HasMaxLength(256);
            });
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("Asset");

                entity.Property(e => e.AssetId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("AssetID")
                    .IsFixedLength(true);

                entity.Property(e => e.AssetName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("CategoryID");

                entity.Property(e => e.InstalledDate).HasColumnType("datetime");

                entity.Property(e => e.LocationId)
                    .HasMaxLength(450)
                    .HasColumnName("LocationID");

                entity.Property(e => e.Specification).HasMaxLength(250);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.LocationId);
            });

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.ToTable("Assignment");

                entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");

                entity.Property(e => e.AdminId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("AdminID");

                entity.Property(e => e.AssetId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("AssetID")
                    .IsFixedLength(true);

                entity.Property(e => e.AssetName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.AssignBy)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AssignTo)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AssignedDate).HasColumnType("datetime");

                entity.Property(e => e.LocationId)
                    .HasMaxLength(450)
                    .HasColumnName("LocationID");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.AssignmentAdmins)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.LocationId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AssignmentUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReturnRequest>(entity =>
            {
                entity.HasKey(e => e.AssignmentId);

                entity.ToTable("ReturnRequest");

                entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");

                entity.Property(e => e.AcceptedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AcceptedUserId)
                    //.IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("AcceptedUserID");

                entity.Property(e => e.RequestBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RequestUserId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("RequestUserID");

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.HasOne(d => d.AcceptedUser)
                    .WithMany(p => p.ReturnRequestAcceptedUsers)
                    .HasForeignKey(d => d.AcceptedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Assignment)
                    .WithOne(p => p.ReturnRequest)
                    .HasForeignKey<ReturnRequest>(d => d.AssignmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RequestUser)
                    .WithMany(p => p.ReturnRequestRequestUsers)
                    .HasForeignKey(d => d.RequestUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("UserID");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.JoinedDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LocationId)
                    .HasMaxLength(450)
                    .HasColumnName("LocationID");

                entity.Property(e => e.StaffCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LocationId);
            });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany("Users")
                .UsingEntity<IdentityUserRole<string>>(
                    userRole => userRole.HasOne<IdentityRole>()
                        .WithMany()
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired(),
                    userRole => userRole.HasOne<User>()
                        .WithMany()
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired());
        }
    }
}
