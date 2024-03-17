using Identity.Service.Implementation;
using Identity.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace Identity.Service
{
    public static class DependencyInjection
    {
        public static void AddPetProjectIdentityService(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }
    }
}
