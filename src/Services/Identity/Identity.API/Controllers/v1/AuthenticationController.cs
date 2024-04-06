using Identity.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TokenManageHandler;
using TokenManageHandler.Models;

namespace Identity.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthenticationController(IUserService userService, JwtTokenHandler jwtTokenHandler) : Controller
    {
        [HttpPost]
        public IActionResult Login([FromBody] AuthenticationRequest request)
        {
            Console.WriteLine("secretkey", Environment.GetEnvironmentVariable("PET_PROJECT_JWT_SECURITY_KEY"));
            var user = userService.Login(request.UserName, request.Password);
            var authen = jwtTokenHandler.GenerateJwtToken(user);
            return Json(authen);
        }
    }
}
