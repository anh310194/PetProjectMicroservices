using Identity.Service.Interfaces;
using Identity.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
