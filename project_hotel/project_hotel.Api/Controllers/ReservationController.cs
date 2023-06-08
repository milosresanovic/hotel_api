using Bogus.DataSets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_hotel.Application.UseCases.Commands;
using project_hotel.Application.UseCases.DTO;
using project_hotel.Application.UseCases.DTO.Searches;
using project_hotel.Application.UseCases.Queries;
using project_hotel.DataAccess;
using project_hotel.Implementation;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project_hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private UseCaseHandler _handler;

        public ReservationController(UseCaseHandler handler)
        {
            _handler = handler;
        }



        /// <summary>
        /// Returns apartments and filter them.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /api/reservation?keyword=korinik&priceFrom=12&priceTo=40
        /// </remarks>
        /// <response code="200">Returns filtered apartments</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Unexpected server error.</response>
        /// 
        // GET: api/<ReservationController>
        [HttpGet]
        public IActionResult Get([FromQuery]SearchReservationsDto request, [FromServices]IGetReservationsQuery query)
        {
            var response = _handler.HandleQuery(query, request);
            return Ok(response);
        }


        /// <summary>
        /// Creates new equipment.
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST 
        ///     {
        ///         "ApartmentId" : 1,
        ///         "UserId" : 1,
        ///         "PersonsNumber" : 3,
        ///         "DateFrom" : "2023-05-05",
        ///         "DateTo" : "2023-05-10"
        ///     }
        /// </remarks>
        /// <response code="201">Successfully created new reservation</response>
        /// <response code="422">Validation exception</response>
        /// <response code="500">Server Error</response>
        // POST api/<ReservationController>
        [HttpPost]
        public IActionResult Post([FromBody] ReservationDto data,
                                  [FromServices]ICreateReservationCommand command)
        {
            _handler.HandleCommand(command, data);

            return StatusCode(201);
        }


        /// <summary>
        /// Deletes reservation.
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE /api/reservation/3
        /// </remarks>
        /// <response code="204">Successfully deleted reservation</response>
        /// <response code="404">There is no reservation with given id</response>
        /// <response code="409">Reservation already started</response>
        /// <response code="500">Server Error</response>
        // DELETE api/<ReservationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices]IDeleteReservationCommand command)
        {
            _handler.HandleCommand(command, id);
            
            return NoContent();
        }
    }
}
