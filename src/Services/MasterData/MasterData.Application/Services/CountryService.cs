using MasterData.Application.Interfaces;
using MasterData.Application.Models;
using MasterData.Domain.Entities;
using MasterData.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Utilities;

namespace MasterData.Application.Services
{
    public class CountryService(IUnitOfWork unitOfWork, ICacheService cacheService) : ICountryService
    {
        private string GetKeyCache(int id)
        {
            return $"Country:{id}";
        }

        public async Task<CountryResponseModel> AddCountry(CountryModel country, int userId)
        {
            var countryInserted = unitOfWork.CountryRepository.Insert(new Country() { Code = country.Code, Name = country.Name, Status = country.Status }, userId);
            await unitOfWork.SaveChangesAsync();
            return new CountryResponseModel() { Code = countryInserted.Code, Name = countryInserted.Name, Status = countryInserted.Status, Id = countryInserted.Id, RowVersion = countryInserted.RowVersion };
        }

        public async Task DeleteCountry(int id)
        {
            unitOfWork.CountryRepository.Delete(id);
            await unitOfWork.SaveChangesAsync();
            string key = GetKeyCache(id);
            cacheService.RemoveData(key);
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
            string key = GetKeyCache(id);
            var cachedEntity = cacheService.GetData<CountryResponseModel>(key);
            if (cachedEntity != null)
            {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                return Task.FromResult(cachedEntity);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            }
            return unitOfWork.CountryRepository.FindAsync(id).AsTask().ContinueWith((result) =>
            {
                var country = result.Result;
                if (country == null)
                {
                    throw new NullReferenceException();
                }
                var countryModel = new CountryResponseModel() { Code = country.Code, Name = country.Name, Id = country.Id, Status = country.Status, RowVersion = country.RowVersion };
                DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
                cacheService.SetData(key, countryModel, expirationTime);
                return countryModel;
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
            string key = GetKeyCache(id);
            cacheService.RemoveData(key);
            return await unitOfWork.CountryRepository.FindAsync(id).AsTask().ContinueWith((result) =>
            {
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
