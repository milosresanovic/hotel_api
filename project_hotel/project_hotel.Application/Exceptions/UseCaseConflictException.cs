using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.Exceptions
{
    public class UseCaseConflictException : Exception
    {
        public UseCaseConflictException(string message) : base(message)
        {

        }
    }
}
