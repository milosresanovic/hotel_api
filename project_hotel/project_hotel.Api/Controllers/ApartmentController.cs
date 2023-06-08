using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_hotel.Api.Core.Dto;
using project_hotel.Application.UseCases.Commands;
using project_hotel.Application.UseCases.DTO;
using project_hotel.Application.UseCases.DTO.Searches;
using project_hotel.Application.UseCases.Queries;
using project_hotel.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project_hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApartmentController : ControllerBase
    {
        private UseCaseHandler _handler;

        public static IEnumerable<string> AllowedExtensions =>
            new List<string> { ".jpg", ".png", ".jpeg" };

        public ApartmentController(UseCaseHandler handler)
        {
            _handler = handler;
        }


        /// <summary>
        /// Returns apartments and filter them.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /api/apartment?keyword=apartman&maxpersons=3&minPrice=30&maxPrice=100&perPage=7
        /// </remarks>
        /// <response code="200">Returns filtered apartments</response>
        /// <response code="500">Unexpected server error.</response>
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery]SearchApartmentsDto search, [FromServices]IGetApartmentsQuery query)
        {
            var result = _handler.HandleQuery(query, search);
            return Ok(result);
        }


        /// <summary>
        /// Finds aparmtent by identifier.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /api/apartment/3
        /// </remarks>
        /// <response code="200">Successfully return specific apartment</response>
        /// <response code="404">There is no apartment with specific Id</response>
        /// <response code="500">Server Error</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices]IFindApartmentQuery command)
        {
            var result =_handler.HandleQuery(command, id);
            return Ok(result);
        }


        /// <summary>
        /// Create new apartment.
        /// </summary>
        /// <remarks>
        /// Form Params:
        /// <b>- Id:</b> Identifier of apartment. <br/>
        /// <b>- Name:</b> Name of aparmtent <br/>
        /// <b>- Description:</b> Description of apartment <br/>
        /// <b>- MaxPersons:</b> Number of maximum kapacity <br/>
        /// <b>- Price:</b> Decimal number <br/>
        /// <b>- CategoryId:</b> Category identifier <br/>
        /// <b>- Equipmentsme:</b> Array of integers <br/>
        /// <b>- Rooms:</b> Array of integers <br/>
        /// <b>- Images:</b> Choose file... <br/>
        /// </remarks>
        /// <response code="204">Successfully changed apartment info</response>
        /// <response code="422">Validation failure</response>
        /// <response code="500">Unexpected Server Error</response>
        [HttpPost]
        public IActionResult Post([FromForm] CreateApartmentWithImagesDto request,
                                  [FromServices]ICreateApartmentCommand command)
        {
            if (request.Images != null)
            {
                foreach(var i in request.Images)
                {
                    var guid = Guid.NewGuid().ToString();

                    var extension = Path.GetExtension(i.FileName);

                    if (!AllowedExtensions.Contains(extension))
                    {
                        throw new InvalidOperationException("Unsupported file type.");
                    }

                    var fileName = guid + extension;

                    var filePath = Path.Combine("wwwroot", "images", fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    i.CopyTo(stream);


                    request.ImagePaths.Add(fileName);
                }
            }

            _handler.HandleCommand(command, request);

            return StatusCode(201);
        }


        /// <summary>
        /// Changes apartment specifications.
        /// </summary>
        /// <remarks>
        /// <b>- Id:</b> Identifier of apartment. <br/>
        /// <b>- Name:</b> Name of aparmtent <br/>
        /// <b>- Description:</b> Description of apartment <br/>
        /// <b>- MaxPersons:</b> Number of maximum kapacity <br/>
        /// <b>- Price:</b> Decimal number <br/>
        /// <b>- CategoryId:</b> Category identifier <br/>
        /// <b>- Equipmentsme:</b> Array of integers <br/>
        /// <b>- Rooms:</b> Array of integers <br/>
        /// <b>- Images:</b> Choose file... <br/>
        /// </remarks>
        /// <response code="204">Successfully changed apartment info</response>
        /// <response code="422">Validation failure</response>
        /// <response code="500">Unexpected Server Error</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] UpdateApartmentWithImagesDto request, [FromServices] IUpdateApartmentCommand command)
        {
            if (request.Images != null)
            {
                foreach (var i in request.Images)
                {
                    var guid = Guid.NewGuid().ToString();

                    var extension = Path.GetExtension(i.FileName);

                    if (!AllowedExtensions.Contains(extension))
                    {
                        throw new InvalidOperationException("Unsupported file type.");
                    }

                    var fileName = guid + extension;

                    var filePath = Path.Combine("wwwroot", "images", fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    i.CopyTo(stream);


                    request.ImagePaths.Add(fileName);
                }
            }



            request.Id = id;
            _handler.HandleCommand(command, request);
            return NoContent();
        }


        /// <summary>
        /// Deactivate apartment so new reservations are not allowed.
        /// </summary>
        /// <remarks>
        /// Sample request: PATCH /api/apartment/3
        /// </remarks>
        /// <response code="204">Successfully deactivated apartment</response>
        /// <response code="404">There is no apartment with specific Id</response>
        /// <response code="500">Unexpected Server Error</response>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromServices]IDeactivateApartmentCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }

        
    }
}
