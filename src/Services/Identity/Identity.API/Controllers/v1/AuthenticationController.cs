﻿using Identity.Service.Interfaces;
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
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
        {
            Console.WriteLine("secretkey", JwtTokenHandler.JWT_SECURITY_KEY);
            var user = await userService.Login(request.UserName, request.Password);
            var authen = jwtTokenHandler.GenerateJwtToken(user);
            return Json(authen);
        }
    }
}
