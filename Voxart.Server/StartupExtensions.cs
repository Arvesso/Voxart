using Microsoft.EntityFrameworkCore;
using Voxart.Lib.Database;

namespace Voxart.Server
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connection)
        {
            services.AddMySql<AppDbContext>(connection, new MySqlServerVersion(new Version(8, 0, 34)));
            return services;
        }

        public static (WebApplicationBuilder builder, ConfigurationManager config) CreateBuilder()
        {
            var builder = WebApplication.CreateBuilder();
            var config = builder.Configuration;
            return (builder, config);
        }
    }
}
