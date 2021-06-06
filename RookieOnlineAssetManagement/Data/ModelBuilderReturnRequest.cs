using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Data
{
    public class ModelBuilderReturnRequest
    {
        public static void Build(ModelBuilder modelBuilder)
        {
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
        }
    }
}
