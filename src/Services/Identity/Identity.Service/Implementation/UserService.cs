using Identity.Core;
using Identity.Core.Interfaces;
using Identity.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using TokenManageHandler.Models;

namespace Identity.Service.Implementation
{
    public class UserService(IUnitOfWork _unitOfWork) : IUserService
    {
        public async Task<UserAccount> Login(string username, string password)
        {
            var user = await _unitOfWork.UserRepository.Queryable(p => p.UserName == username).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("The username could not be found.");
            }
            if (!Helper.VerifyPassword(password, user.Password ?? "", user.SaltPassword ?? ""))
            {
                throw new Exception("The password incorrect.");
            }
            var role = await _unitOfWork.RoleRepository.Queryable(p => user.RoleId == p.Id).FirstOrDefaultAsync();
            if (role == null)
            {
                throw new Exception("The role within user is not exists.");
            }
            return new UserAccount() { DisplayName = $"{user.FirstName} {user.LastName}", Role = role.Code, UserName = username, Address = user.Address, Avatar = user.AvatarUrl };
        }
    }
}
