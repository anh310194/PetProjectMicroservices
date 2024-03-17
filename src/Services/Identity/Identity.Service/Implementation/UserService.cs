using Identity.Service.Interfaces;
using TokenManageHandler.Models;

namespace Identity.Service.Implementation
{
    public class UserService : IUserService
    {
        public UserAccount Login(string username, string password)
        {
            return new UserAccount() { DisplayName = "Anh Nguyen", Role = "Administrator", UserName = "anh.nguyen", Address = "address", Avatar = "avatar test" };
        }
    }
}
