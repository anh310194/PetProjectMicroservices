using MasterData.Application.Interfaces;
using MasterData.Application.Models;
using MasterData.Domain.Entities;
using MasterData.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Utilities;

namespace MasterData.Application.Services
{
    public class StateService(IUnitOfWork unitOfWork, ICacheService cacheService) : IStateService
    {
        private string GetKeyCache(int id)
        {
            return $"State:{id}";
        }

        public async Task<StateResponseModel> AddState(StateModel State, int userId)
        {
            var StateInserted = unitOfWork.StateRepository.Insert(new State() { Code = State.Code, Name = State.Name, Status = State.Status, CountryId = State.CountryId }, userId);
            await unitOfWork.SaveChangesAsync();
            return new StateResponseModel() { Code = StateInserted.Code, Name = StateInserted.Name, Status = StateInserted.Status, Id = StateInserted.Id, RowVersion = StateInserted.RowVersion };
        }

        public async Task DeleteState(int id)
        {
            unitOfWork.StateRepository.Delete(id);
            await unitOfWork.SaveChangesAsync();
            string key = GetKeyCache(id);
            cacheService.RemoveData(key);
        }

        public Task<List<StateResponseModel>> GetAll()
        {
            return unitOfWork.StateRepository.Queryable().ToListAsync().ContinueWith((result) =>
             {
                 return result.Result.Select((State) =>
                 {
                     return new StateResponseModel() { Code = State.Code, Name = State.Name, Id = State.Id, Status = State.Status, RowVersion = State.RowVersion };
                 }).ToList();
             });


        }

        public Task<StateResponseModel> GetById(int id)
        {
            string key = GetKeyCache(id);
            var cachedEntity = cacheService.GetData<StateResponseModel>(key);
            if (cachedEntity != null)
            {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                return Task.FromResult(cachedEntity);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            }
            return unitOfWork.StateRepository.FindAsync(id).AsTask().ContinueWith((result) =>
            {
                var State = result.Result;
                if (State == null)
                {
                    throw new NullReferenceException();
                }
                var StateModel = new StateResponseModel() { Code = State.Code, Name = State.Name, Id = State.Id, Status = State.Status, RowVersion = State.RowVersion };
                DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
                cacheService.SetData(key, StateModel, expirationTime);
                return StateModel;
            });
        }

        public async Task<StateResponseModel> UpdateStatus(int id, EnumStatus status)
        {
            var State = await unitOfWork.StateRepository.FindAsync(id);
            if (State == null)
            {
                throw new NullReferenceException();
            }
            if (State.Status == status)
            {
                throw new ApplicationException($"State {id} is setten {status}!");
            }
            State.Status = status;
            unitOfWork.StateRepository.Update(State, 1);
            await unitOfWork.SaveChangesAsync();
            string key = GetKeyCache(id);
            cacheService.RemoveData(key);
            return await unitOfWork.StateRepository.FindAsync(id).AsTask().ContinueWith((result) =>
            {
                var State = result.Result;
                if (State == null)
                {
                    throw new NullReferenceException();
                }
                return new StateResponseModel() { Code = State.Code, Name = State.Name, Id = State.Id, Status = State.Status, RowVersion = State.RowVersion };
            });

        }
    }
}
