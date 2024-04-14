using Identity.Core;
using Identity.Core.Interfaces;
using Identity.Core.Models;
using Microsoft.EntityFrameworkCore;
using Utilities;

namespace Identity.Infrastructure
{
    public static class SeedData
    {
        public async static Task AddSeedDataAsync(IUnitOfWork unitOfWork)
        {
            if (await unitOfWork.UserRepository.Queryable().FirstOrDefaultAsync() == null)
            {
                var saltPassword = Helper.GenerateSalt();
                unitOfWork.UserRepository.Insert(
                    new User()
                    {
                        FirstName = "System",
                        LastName = "Admin",
                        UserName = "sysadmin",
                        Password = Helper.HashPassword("sysadmin", saltPassword),
                        SaltPassword = saltPassword,
                        Status = EnumStatus.Activate,
                        UserType = EnumUserType.SystemAdministrator,
                    },
                    1
                );

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
