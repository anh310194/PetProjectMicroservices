using MasterData.Domain.Interfaces;
using MasterData.Infrastructure.Persistence;
using MasterData.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MasterData.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>(sp =>
                            ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")));
#pragma warning restore CS8604 // Possible null reference argument.
            services.AddDbContext<MasterDataContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheService, RedisCacheService>();
        }
    }
}
