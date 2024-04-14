using Identity.Core;
using Identity.Core.Interfaces;
using Identity.Core.Models;
using Identity.Service.Interfaces;
using Identity.Service.Models;
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
            return new UserAccount() { DisplayName = $"{user.FirstName} {user.LastName}", UserType = user.UserType.ToString(), UserName = user.UserName, Address = user.Address, Avatar = user.AvatarUrl };
        }

        public async Task<UserAccount> RegisterUser(RegisterUserModel model)
        {
            if (await _unitOfWork.UserRepository.Queryable(p => p.UserName == model.Email).AnyAsync())
            {
                throw new Exception("The email is exist in the system.");
            }
            int userId = 1;

            var tenant = _unitOfWork.TenantRepository.Insert(new() { Name = model.TenantName }, userId);

            var saltPassword = Helper.GenerateSalt();
            var user = _unitOfWork.UserRepository.Insert(new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Status = Utilities.EnumStatus.Activate,
                UserName = model.Email,
                Address = model.Address,
                CountryId = model.CountryId,
                StateId = model.StateId,
                TenantId = tenant.Id,
                UserType = EnumUserType.Admin,
                Tenant = tenant
            }, userId);


            await _unitOfWork.SaveChangesAsync();

            return new UserAccount() { DisplayName = $"{user.FirstName} {user.LastName}", UserType = user.UserType.ToString(), UserName = user.UserName, Address = user.Address, Avatar = user.AvatarUrl };
        }
    }
}
