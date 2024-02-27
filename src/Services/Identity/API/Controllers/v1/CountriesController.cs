using Identity.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CountriesController : ControllerBase
{
    private readonly IUnitOfWork _UnitOfWork;
    public CountriesController(IUnitOfWork unitOfWork)
    {
        this._UnitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<ActionResult<List<Country>>> GetAll()
    {
        return await _UnitOfWork.CountryRepository.Queryable().ToListAsync();
    }
}