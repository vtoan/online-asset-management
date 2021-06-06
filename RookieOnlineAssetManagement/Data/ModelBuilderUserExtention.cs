using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Data
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
