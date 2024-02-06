using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CountriesController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> GetAll()
    {
        return "Get All Countries v1";
    }
}