using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Entities;

namespace RookieOnlineAssetManagement.Data.ModelBuilders
{
    public class ModelBuilderAssignment
    {
        public static void Build(ModelBuilder modelBuilder)
        {
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
        }
    }
}
