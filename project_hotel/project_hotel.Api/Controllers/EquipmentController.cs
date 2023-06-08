using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_hotel.Application.UseCases.Commands;
using project_hotel.Application.UseCases.DTO;
using project_hotel.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project_hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private UseCaseHandler _handler;

        public EquipmentController(UseCaseHandler handler)
        {
            _handler = handler;
        }


        /// <summary>
        /// Creates new equipment.
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST 
        ///     {
        ///         "Name" : "Novi equipment"
        ///     }
        /// </remarks>
        /// <response code="201">Successfully created new equipment</response>
        /// <response code="422">Validation exception</response>
        /// <response code="500">Server Error</response>
        // POST api/<EquipmentController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] EquipmentDto data, [FromServices] ICreateEquipmentCommand command)
        {
            _handler.HandleCommand(command, data);

            return StatusCode(201);
        }
    }
}
