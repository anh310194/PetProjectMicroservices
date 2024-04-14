using Identity.Service.Models;
using TokenManageHandler.Models;

namespace Identity.Service.Interfaces
{
    public interface IUserService
    {
        public Task<UserAccount> Login(string username, string password);
        public Task<UserAccount> RegisterUser(RegisterUserModel model);
    }
}
