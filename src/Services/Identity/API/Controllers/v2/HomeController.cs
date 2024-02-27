using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers.v2;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> GetAll()
    {
        return "Home v2";
    }
}