using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string response) : base(response)
        {

        }
    }
}
