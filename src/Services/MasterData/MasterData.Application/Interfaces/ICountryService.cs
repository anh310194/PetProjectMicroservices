
using MasterData.Application.Models;
using MasterData.Domain;

namespace MasterData.Application.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryResponseModel>> GetAll();
        Task<CountryResponseModel> GetById(int id);
        Task<CountryResponseModel> AddCountry(CountryModel country, int userId);
        Task<CountryResponseModel> UpdateStatus(int id, EnumStatus status);
        Task DeleteCountry(int id);
    }
}
