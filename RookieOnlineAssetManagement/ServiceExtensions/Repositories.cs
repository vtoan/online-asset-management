using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Repositories;

namespace RookieOnlineAssetManagement.ServiceExtensions
{
    public static class Repositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IReturnRequestRepository, ReturnRequestRepository>();
            return services;
        }
    }
}