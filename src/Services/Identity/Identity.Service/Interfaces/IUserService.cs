using TokenManageHandler.Models;

namespace Identity.Service.Interfaces
{
    public interface IUserService
    {
        public UserAccount Login(string username, string password);
    }
}
