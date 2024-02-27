using Core.Interfaces;
using MasterData.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterData.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CountryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public CountryController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<List<CountryResponseDTO>>> GetAll()
        {
            var countries = await _unitOfWork.CountryRepository.Queryable().ToListAsync();

            return countries.Select((country) =>
            {
                return new CountryResponseDTO() { Code = country.Code, Name = country.Name, Id = country.Id, Status = country.Status };
            }).ToList();
        }
    }
}
