
using MasterData.Application.Models;
using Utilities;

namespace MasterData.Application.Interfaces
{
    public interface IStateService
    {
        Task<List<StateResponseModel>> GetAll();
        Task<StateResponseModel> GetById(int id);
        Task<StateResponseModel> AddState(StateModel State, int userId);
        Task<StateResponseModel> UpdateStatus(int id, EnumStatus status);
        Task DeleteState(int id);
    }
}
