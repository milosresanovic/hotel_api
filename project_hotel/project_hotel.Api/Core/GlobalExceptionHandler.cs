using FluentValidation;
using project_hotel.Application.Exceptions;
using project_hotel.Application.Logging.Exceptions;

namespace project_hotel.Api.Core
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionLogger _logger;

        public GlobalExceptionHandler(RequestDelegate next, IExceptionLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                _logger.Log(ex);

                httpContext.Response.ContentType = "application/json";
                object response = null;
                var statusCode = StatusCodes.Status500InternalServerError;

                if (ex is EntityNotFoundException)
                {
                    statusCode = StatusCodes.Status404NotFound;
                }

                if(ex is UnprocessableEntityException ee)
                {
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    response = new { message = ee.Message };
                }

                if (ex is ValidationException e)
                {
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    response = new
                    {
                        errors = e.Errors.Select(x => new
                        {
                            property = x.PropertyName,
                            error = x.ErrorMessage
                        })
                    };
                }

                if(ex is ConflictException ce)
                {
                    statusCode = StatusCodes.Status409Conflict;
                    response = new {message = ce.Message};
                }

                if(ex is UnauthorizedAccessException)
                {
                    statusCode = StatusCodes.Status401Unauthorized;
                }

                if (ex is UseCaseConflictException conflictEx)
                {
                    statusCode = StatusCodes.Status409Conflict;
                    response = new { message = conflictEx.Message };
                }


                httpContext.Response.StatusCode = statusCode;
                if (response != null)
                {
                    await httpContext.Response.WriteAsJsonAsync(response);
                }
            }
        }
    }
}
