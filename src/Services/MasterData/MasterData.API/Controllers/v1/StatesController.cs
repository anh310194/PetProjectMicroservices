using MasterData.Application.Interfaces;
using MasterData.Application.Models;
using MasterData.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MasterData.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class StatesController(IStateService StateService) : Controller
    {

        [HttpGet]
        public async Task<ActionResult<List<StateResponseModel>>> GetAll()
        {
            return await StateService.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<StateResponseModel>> GetById(int id)
        {
            return await StateService.GetById(id);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StateResponseModel>> Add(StateModel State)
        {
            var StateInserted = await StateService.AddState(State, 1);
            return StatusCode(StatusCodes.Status201Created, StateInserted);
        }


        [HttpPatch("activate/{id}")]
        public async Task<ActionResult<StateResponseModel>> Activate(int id)
        {
            return await StateService.UpdateStatus(id, EnumStatus.Activate);
        }


        [HttpPatch("Inactivate/{id}")]
        public async Task<ActionResult<StateResponseModel>> InActivate(int id)
        {
            return await StateService.UpdateStatus(id, EnumStatus.InActivate);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await StateService.DeleteState(id);
            return NoContent();
        }
    }
}
