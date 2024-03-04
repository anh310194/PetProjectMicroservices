using MasterData.Application.Interfaces;
using MasterData.Application.Models;
using MasterData.Domain;
using MasterData.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MasterData.Application.Services
{
    public class CountryService(IUnitOfWork unitOfWork) : ICountryService
    {
        public Task<CountryResponseModel> AddCountry(CountryModel country)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCountry(int id)
        {
            unitOfWork.CountryRepository.Delete(id);
            await unitOfWork.SaveChangesAsync();
        }

        public Task<List<CountryResponseModel>> GetAll()
        {
           return unitOfWork.CountryRepository.Queryable().ToListAsync().ContinueWith((result) =>
            {
                return result.Result.Select((country) =>
                {
                    return new CountryResponseModel() { Code = country.Code, Name = country.Name, Id = country.Id, Status = country.Status, RowVersion = country.RowVersion };
                }).ToList();
            });

            
        }

        public Task<CountryResponseModel> GetById(int id)
        {
            return unitOfWork.CountryRepository.FindAsync(id).AsTask().ContinueWith((result) =>{
                var country = result.Result;
                if (country == null)
                {
                    throw new NullReferenceException();
                }
                return new CountryResponseModel() { Code = country.Code, Name = country.Name, Id = country.Id, Status = country.Status, RowVersion = country.RowVersion };
            });
        }

        public async Task<CountryResponseModel> UpdateStatus(int id, EnumStatus status)
        {
            var country = await unitOfWork.CountryRepository.FindAsync(id);
            if (country == null)
            {
                throw new NullReferenceException();
            }
            if (country.Status == status)
            {
                throw new ApplicationException($"Country {id} is setten {status}!");
            }
            country.Status = status;
            unitOfWork.CountryRepository.Update(country, 1);
            await unitOfWork.SaveChangesAsync();
            return await unitOfWork.CountryRepository.FindAsync(id).AsTask().ContinueWith((result) => {
                var country = result.Result;
                if (country == null)
                {
                    throw new NullReferenceException();
                }
                return new CountryResponseModel() { Code = country.Code, Name = country.Name, Id = country.Id, Status = country.Status, RowVersion = country.RowVersion };
            });

        }
    }
}
