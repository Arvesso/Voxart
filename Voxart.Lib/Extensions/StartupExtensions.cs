using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Voxart.Lib.Database;

namespace Voxart.Lib.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connection)
        {
            services.AddMySql<AppDbContext>(connection, new MySqlServerVersion(new Version(8, 0, 34)));
            return services;
        }
    }
}
