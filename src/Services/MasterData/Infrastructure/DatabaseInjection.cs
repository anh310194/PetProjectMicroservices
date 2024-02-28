using Core.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DatabaseInjection
    {
        public static void AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MasterDataContext>(opt => opt.UseMySQL(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
