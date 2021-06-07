using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Entities;

namespace RookieOnlineAssetManagement.Data.ModelBuilders
{
    public class ModelBuilderAsset
    {
        public static void Build(ModelBuilder modelBuilder)
        {
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
        }
    }
}
