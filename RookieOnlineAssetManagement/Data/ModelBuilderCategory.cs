using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Entities;

namespace RookieOnlineAssetManagement.Data
{
    public class ModelBuilderCategory
    {
        public static void Build(ModelBuilder modelBuilder)
        {
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
        }
    }
}
