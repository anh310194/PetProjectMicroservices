using Core.Interfaces;
using MasterData.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterData.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CountriesController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public CountriesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<List<CountryResponseDTO>>> GetAll()
        {
            var countries = await _unitOfWork.CountryRepository.Queryable().ToListAsync();

            return countries.Select((country) =>
            {
                return new CountryResponseDTO() { Code = country.Code, Name = country.Name, Id = country.Id, Status = country.Status, RowVersion = country.RowVersion };
            }).ToList();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CountryResponseDTO>> GetById(int id)
        {
            var country = await _unitOfWork.CountryRepository.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            return new CountryResponseDTO() { Code = country.Code, Name = country.Name, Id = country.Id, Status = country.Status, RowVersion = country.RowVersion };
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountryResponseDTO>> Add(CountryDTO country)
        {
            var resultEntry = await _unitOfWork.CountryRepository.InsertAsync(new Country() { Code = country.Code, Status = country.Status, Name = country.Name }, 1);
            await _unitOfWork.SaveChangesAsync();
            if (resultEntry == null)
            {
                // Return 400 Bad Request to indicate that the insertion failed
                return BadRequest();
            }

            // Get the inserted country from the EntityEntry
            Country result = resultEntry.Entity;

            var countryDTO = new CountryResponseDTO() { Code = result.Code, Name = result.Name, Id = result.Id, Status = result.Status, RowVersion = result.RowVersion };
            return StatusCode(StatusCodes.Status201Created, countryDTO);

        }


        [HttpPatch("/activate/{id}")]
        public async Task<ActionResult<CountryResponseDTO>> Activate(int id)
        {
            var country = await _unitOfWork.CountryRepository.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            if (country.Status == Core.EnumStatus.Activate)
            {
                return BadRequest($"Country {id} is activated!");
            }
            country.Status = Core.EnumStatus.Activate;
            _unitOfWork.CountryRepository.Update(country, 1);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }


        [HttpPatch("/Inactivate/{id}")]
        public async Task<ActionResult<CountryResponseDTO>> InActivate(int id)
        {
            var country = await _unitOfWork.CountryRepository.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            if (country.Status == Core.EnumStatus.InActivate)
            {
                return BadRequest($"Country {id} is inactivated!");
            }
            country.Status = Core.EnumStatus.InActivate;
            _unitOfWork.CountryRepository.Update(country, 1);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("/Delete/{id}")]
        public async Task<ActionResult<CountryResponseDTO>> InActivate(int id, byte[] RowVersion)
        {
            var country = await _unitOfWork.CountryRepository.Queryable(p => p.Id == id && p.RowVersion.SequenceEqual(RowVersion)).FirstOrDefaultAsync();
            if (country == null)
            {
                return NotFound();
            }
            _unitOfWork.CountryRepository.Delete(country);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
