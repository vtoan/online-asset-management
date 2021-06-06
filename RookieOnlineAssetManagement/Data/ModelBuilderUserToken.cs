using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RookieOnlineAssetManagement.Data
{
    public class ModelBuilderUserToken
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }
    }
}
