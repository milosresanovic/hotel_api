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
    public class CommentController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public CommentController(UseCaseHandler handler)
        {
            _handler = handler;
        }




        /// <summary>
        /// Creates new comment
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST 
        ///     {
        ///         "ApartmentId" : 3 <br/>
        ///         "UserId" : 12 <br/>
        ///         "Text" : "String" <br/>
        ///         "StarNumber" : 5 <br/>
        ///     }
        /// </remarks>
        /// <response code="201">Successfully created comment</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>

        // POST api/<CommentController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateCommentDto request, [FromServices]ICreateCommentCommand command)
        {
            _handler.HandleCommand(command, request);
            return Ok();
        }

        /// <summary>
        /// Deletes comment from database
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE /api/comment/3
        /// </remarks>
        /// <response code="204">Successfully delete comment</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">There is no comment with specific Id</response>
        /// <response code="500">Unexpected Server Error</response>

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices]IDeleteCommentCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
