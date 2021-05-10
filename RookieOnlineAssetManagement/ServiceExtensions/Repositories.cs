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

            return services;
        }
    }
}