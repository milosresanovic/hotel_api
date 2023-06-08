using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.Logging.UseCases
{
    public interface IUseCaseLogger
    {
        void Log(UseCaseLog log);
        IEnumerable<UseCaseLog> GetLogs(UseCaseLogSearch search);
    }
}
