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
    public class UserController : ControllerBase
    {
        public static IEnumerable<string> AllowedExtensions =>
            new List<string> { ".jpg", ".png", ".jpeg" };

        private readonly UseCaseHandler _useCaseHandler;       

        public UserController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }


        /// <summary>
        /// Gets filtered users
        /// </summary>
        /// <remarks>
        /// Sample request: GET /api/user?keyword=Korisnik
        /// </remarks>
        /// <response code="200">Successfully returned users</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Unexpected Server Error</response>

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get([FromQuery]SearchUsersDto request, [FromServices]IGetUsersQuery query)
        {
            var result = _useCaseHandler.HandleQuery(query, request);
            return Ok(result);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }


        /// <summary>
        /// Creates user.
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST /api/user 
        ///     {
        ///         "Username" : "username", <br/>
        ///         "FirstName" : "FirstName", <br/>
        ///         "LastName" : "LastName", <br/>
        ///         "Email" : "email@gmail.com", <br/>
        ///         "Password" : "Sifra123!" <br/>
        ///     }
        /// </remarks>
        /// <response code="201">Successfully deleted (IsActiv = false, DeletedAt = UTC.now)</response>
        /// <response code="400">Validation error</response>
        /// <response code="409">Conflict, Email or Username already exists.</response>
        /// <response code="500">Server Error</response>
        // POST api/<UserController>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromForm] CreateUserWithImageDto request, [FromServices]ICreateUserCommand command)
        {
            if (request.Image != null)
            {
                var guid = Guid.NewGuid().ToString();

                var extension = Path.GetExtension(request.Image.FileName);

                if (!AllowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Unsuppoerted file type.");
                }

                var fileName = guid + extension;

                var filePath = Path.Combine("wwwroot", "images", fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                request.Image.CopyTo(stream);

                request.ImagePath = filePath;
            }
            

            _useCaseHandler.HandleCommand(command, request);

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] UpdateUserWithImage request, [FromServices] IUpdateUserCommand command)
        {
            if (request.Image != null)
            {
                var guid = Guid.NewGuid().ToString();

                var extension = Path.GetExtension(request.Image.FileName);

                if (!AllowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Unsuppoerted file type.");
                }

                var fileName = guid + extension;

                var filePath = Path.Combine("wwwroot", "images", fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                request.Image.CopyTo(stream);

                request.ImagePath = filePath;
            }

            request.Id = id;

            _useCaseHandler.HandleCommand(command, request);

            return StatusCode(StatusCodes.Status204NoContent);
        }



        /// <summary>
        /// Delete user.
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE /api/user/6
        /// </remarks>
        /// <response code="204">Successfully deleted (IsActiv = false, DeletedAt = UTC.now)</response>
        /// <response code="401">Unouthorized.</response>
        /// <response code="404">There is no user with specific Id.</response>
        /// <response code="409">Conflict, User has future reservations</response>
        /// <response code="500">Server Error</response>
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        
        public IActionResult Delete(int id, [FromServices]IDeleteUserCommand command)
        {
            _useCaseHandler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
