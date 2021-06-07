using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Entities;

namespace RookieOnlineAssetManagement.Data.ModelBuilders
{
    public class ModelBuilderUserExtention
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserExtension>(entity =>
            {
                entity.ToTable("UserExtension");

                entity.Property(e => e.UserName)
                    .IsUnicode(true)
                    .HasMaxLength(256);
            });
        }
    }
}
