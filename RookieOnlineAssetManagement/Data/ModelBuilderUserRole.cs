using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RookieOnlineAssetManagement.Data
{
    public class ModelBuilderUserRole
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        }
    }
}
