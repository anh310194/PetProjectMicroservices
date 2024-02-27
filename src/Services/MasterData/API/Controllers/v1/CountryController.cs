using Microsoft.AspNetCore.Mvc;

namespace MasterData.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CountryController : Controller
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "country v1";
        }
    }
}
