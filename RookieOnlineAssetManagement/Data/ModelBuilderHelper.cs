using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Entities;

namespace RookieOnlineAssetManagement.Data
{
    public class ModelBuilderHelper
    {
        public static void Build(ModelBuilder modelBuilder)
        {
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
