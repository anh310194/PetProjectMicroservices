using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.API.Models
{
    public class AuthenticationResponse
    {
        public required string Token { get; set; }
        public required string AuthType { get; set; }
        public required int ExpireIns { get; set; }
    }
}
