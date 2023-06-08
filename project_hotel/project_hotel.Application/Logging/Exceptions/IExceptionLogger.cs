using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.Logging.Exceptions
{
    public interface IExceptionLogger
    {
        void Log(Exception e);
    }
}
