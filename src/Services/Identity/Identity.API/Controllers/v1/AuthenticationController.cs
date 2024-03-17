using Identity.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthenticationController(IUserService userService, JwtTokenHandler jwtTokenHandler) : Controller
    {
        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            var user = userService.Login(userName, password);
            var authen = jwtTokenHandler.GenerateJwtToken(user);
            return Json(authen);
        }
    }
}
