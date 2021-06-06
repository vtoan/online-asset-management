using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RookieOnlineAssetManagement.Data
{
    public class ModelBuilderUserLogin
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        }
    }
}
