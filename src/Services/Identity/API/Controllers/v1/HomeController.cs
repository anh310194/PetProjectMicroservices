using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> GetAll()
    {
        return "Home v1";
    }
}