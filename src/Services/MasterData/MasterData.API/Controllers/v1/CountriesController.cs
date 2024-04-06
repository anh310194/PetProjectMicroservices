using MasterData.Application.Interfaces;
using MasterData.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace MasterData.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CountriesController(ICountryService countryService) : Controller
    {
        [HttpGet]
        public async Task<ActionResult<List<CountryResponseModel>>> GetAll()
        {
            return await countryService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryResponseModel>> GetById(int id)
        {
            return await countryService.GetById(id);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountryResponseModel>> Add(CountryModel country)
        {
            var countryInserted = await countryService.AddCountry(country, 1);
            return StatusCode(StatusCodes.Status201Created, countryInserted);
        }

        [HttpPatch("activate/{id}")]
        public async Task<ActionResult<CountryResponseModel>> Activate(int id)
        {
            return await countryService.UpdateStatus(id, EnumStatus.Activate);
        }

        [HttpPatch("Inactivate/{id}")]
        public async Task<ActionResult<CountryResponseModel>> InActivate(int id)
        {
            return await countryService.UpdateStatus(id, EnumStatus.InActivate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await countryService.DeleteCountry(id);
            return NoContent();
        }
    }
}
