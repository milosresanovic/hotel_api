using Newtonsoft.Json;
using project_hotel.Application.Logging.Exceptions;
using project_hotel.Application.Logging.UseCases;
using project_hotel.Application.UseCases;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace project_hotel.Implementation
{
    public class UseCaseHandler
    {
        private IExceptionLogger _exceptionLoogger;
        private IUseCaseLogger _useCaseLogger;
        private IApplicationUser _user;
        
        public UseCaseHandler(
                IExceptionLogger exceptionLogger,
                IUseCaseLogger useCaseLogger,
                IApplicationUser user)
        {
            this._exceptionLoogger = exceptionLogger;
            this._useCaseLogger = useCaseLogger;
            this._user = user;
        }
        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest data)
        {
            try
            {
                HandleLoggingAndAuthorization(command, data);

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                command.Execute(data);

                stopwatch.Stop();

            }
            catch (Exception e)
            {

                _exceptionLoogger.Log(e);
                throw;
            }

        }

        public TResponse HandleQuery<TRequest, TResponse>(IQuery<TRequest, TResponse> query, TRequest data)
        {
            try
            {
                HandleLoggingAndAuthorization(query, data);

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var response = query.Execute(data);

                stopwatch.Stop(); 

                return response;
            }
            catch (Exception e)
            {

                _exceptionLoogger.Log(e);
                throw;
            }
        }

        public void HandleLoggingAndAuthorization<TRequest>(IUseCase useCase, TRequest data)
        {
            var isAuthorized = _user.UseCaseIds.Contains(useCase.Id);

            var log = new UseCaseLog
            {
                Username = _user.Username,
                ExecutionDateTime = DateTime.UtcNow,
                UseCaseName = useCase.Name,
                IsAuthorized = isAuthorized,
                Data = JsonConvert.SerializeObject(data),
                UserId = _user.Id,
            };

            _useCaseLogger.Log(log);

            if (!isAuthorized)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
