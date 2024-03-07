using MasterData.Application.Interfaces;
using MasterData.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MasterData.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IStateService, StateService>();
        }
    }
}
