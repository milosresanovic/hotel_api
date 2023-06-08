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
    [Authorize]
    public class PriceController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public PriceController(UseCaseHandler handler)
        {
            _handler = handler;
        }


        /// <summary>
        /// Creates new price
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST 
        ///     {
        ///         "ApartmentId" : 3 <br/>
        ///         "StartDate" : "2023-09-09" <br/>
        ///         "Cost" : 120 <br/>
        ///     }
        /// </remarks>
        /// <response code="201">Successfully created comment</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>


        // POST api/<PriceController>
        [HttpPost]
        public IActionResult Post([FromBody] CreatePriceDto request, [FromServices]ICreatePriceCommand command)
        {
            _handler.HandleCommand(command, request);
            return StatusCode(StatusCodes.Status201Created);
        }

    }
}
