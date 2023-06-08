using project_hotel.Application.Logging.UseCases;
using project_hotel.DataAccess;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.Logging
{
    public class DataBaseUseCaseLogger : IUseCaseLogger
    {
        private readonly HotelContext _context;

        public DataBaseUseCaseLogger(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<UseCaseLog> GetLogs(UseCaseLogSearch search)
        {
            throw new NotImplementedException();
        }

        public void Log(UseCaseLog log)
        {
            _context.AuditLogs.Add(new AuditLog
            {
                IsAuthorized = log.IsAuthorized,
                UseCase = log.UseCaseName,
                Username = log.Username,
                UserId = log.UserId,
                Time = log.ExecutionDateTime,
                Data = log.Data
            });

            _context.SaveChanges();
        }
    }
}
