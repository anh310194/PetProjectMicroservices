using MasterData.Domain.Interfaces;
using MasterData.Infrastructure.Persistence;
using MasterData.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MasterData.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnecdtion = configuration.GetConnectionString("RedisConnection");
            Console.WriteLine(redisConnecdtion);
            services.AddStackExchangeRedisCache(op => op.Configuration = redisConnecdtion);
            services.AddDbContext<MasterDataContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheService, RedisCacheService>();
        }


        public static void RunMigration(this IServiceProvider serviceProvider)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MasterDataContext>();
                db.Database.Migrate();
            }
        }
    }
}
