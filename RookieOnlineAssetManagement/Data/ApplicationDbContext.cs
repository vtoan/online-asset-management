using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data.ModelBuilders;
using RookieOnlineAssetManagement.Entities;

namespace RookieOnlineAssetManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ReturnRequest> ReturnRequests { get; set; }
        public DbSet<UserExtension> UserExtensions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ModelBuilderAsset.Build(modelBuilder);
            ModelBuilderAssignment.Build(modelBuilder);
            ModelBuilderCategory.Build(modelBuilder);
            ModelBuilderLocation.Build(modelBuilder);
            ModelBuilderReturnRequest.Build(modelBuilder);
            ModelBuilderRole.Build(modelBuilder);
            ModelBuilderRoleClaim.Build(modelBuilder);
            ModelBuilderUser.Build(modelBuilder);
            ModelBuilderUserClaim.Build(modelBuilder);
            ModelBuilderUserExtention.Build(modelBuilder);
            ModelBuilderUserLogin.Build(modelBuilder);
            ModelBuilderUserRole.Build(modelBuilder);
            ModelBuilderUserToken.Build(modelBuilder);
        }
    }
}