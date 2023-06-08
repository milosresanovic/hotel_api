using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.Exceptions
{
    public class ForbiddenUseCaseExecutionException : Exception
    {
        public ForbiddenUseCaseExecutionException(string useCase, string user) :
            base($"User {user} has tried to execute {useCase} without being authorized to do so.")
        {

        }
    }
}
