using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RookieOnlineAssetManagement.Data
{
    public class ModelBuilderUserClaim
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        }
    }
}
