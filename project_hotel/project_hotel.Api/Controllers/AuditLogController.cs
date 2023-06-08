using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_hotel.Application.UseCases.DTO.Searches;
using project_hotel.Application.UseCases.Queries;
using project_hotel.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project_hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditLogController : ControllerBase
    {
        private UseCaseHandler _handler;

        public AuditLogController(UseCaseHandler handler)
        {
            _handler = handler;
        }


        /// <summary>
        /// Filter and get records from auditlog.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /api/auditlog?keyword=create
        /// </remarks>
        /// <response code="200">Successfully return specific apartment</response>
        /// <response code="500">Server Error</response>

        // GET: api/<AuditLogController>
        [HttpGet]
        public IActionResult Get([FromQuery]SearchAuditLogDto search, [FromServices]IGetAuditLogsQuery query)
        {
            var result = _handler.HandleQuery(query, search);
            return Ok(result);
        }
    }
}
