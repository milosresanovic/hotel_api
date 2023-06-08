using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_hotel.Api.Core;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project_hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtManager _manager;

        public TokenController(JwtManager manager)
        {
            _manager = manager;
        }


        /// <summary>
        /// Generates a token and returns it.
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST 
        ///     {
        ///         "Email" : "email@gmail.com" <br/>
        ///         "Pawword" : "Sifra123!" <br/>
        ///     }
        /// </remarks>
        /// <response code="201">Successfully created a new token</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Server Error</response>
        // POST api/<TokenController>
        [HttpPost]
        public IActionResult Post([FromBody] TokenRequest request)
        {
            try
            {
                var token = _manager.MakeToken(request.Email, request.Password);

                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message});
            }
        }


        /// <summary>
        /// Logout user
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE /api/token 
        /// Auhorization: BearerToken
        /// </remarks>
        /// <response code="204">Successfully invalidated token</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Unexpected Server Error</response>

        [HttpDelete]
        [Authorize]
        public IActionResult InvalidateToken([FromServices] ITokenStorage storage)
        {
            var header = HttpContext.Request.Headers["Authorization"];

            var token = header.ToString().Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            string jti = tokenObj.Claims.FirstOrDefault(x => x.Type == "jti").Value;

            storage.InvalidateToken(jti);

            return NoContent();
        }
    }

    public class TokenRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
