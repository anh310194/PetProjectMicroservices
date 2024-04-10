using Identity.Core.Interfaces;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IdentityContext>(opt => opt.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }


        public static void RunMigration(this IServiceProvider serviceProvider)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();
                db.Database.Migrate();
            }
        }
    }
}
