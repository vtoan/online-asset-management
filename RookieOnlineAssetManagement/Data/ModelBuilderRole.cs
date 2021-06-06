using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RookieOnlineAssetManagement.Data
{
    public class ModelBuilderRole
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}
