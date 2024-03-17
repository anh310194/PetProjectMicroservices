using Identity.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Interfaces
{
    public interface IUserService
    {
        public UserAccount Login(string username, string password);
    }
}
