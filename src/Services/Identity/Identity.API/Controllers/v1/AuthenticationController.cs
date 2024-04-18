using Identity.Service.Interfaces;
using Identity.Service.Models;
using Microsoft.AspNetCore.Mvc;
using TokenManageHandler;
using TokenManageHandler.Models;

namespace Identity.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/authentications")]
    [ApiVersion("1.0")]
    public class AuthenticationController(IUserService userService, JwtTokenHandler jwtTokenHandler) : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
        {
            var user = await userService.Login(request.UserName, request.Password);
            var authen = jwtTokenHandler.GenerateJwtToken(user);
            return Json(authen);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserModel model)
        {
            var userCreated = await userService.RegisterUser(model);
            return Json(userCreated);
        }
    }
}
