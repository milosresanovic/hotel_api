using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.UseCases.DTO
{
    public class AuditLogDto : BaseDto
    {
        public string? Username { get; set; }
        public DateTime Time { get; set; }
        public string? UseCase { get; set; }
        public string? IsAuthorized { get; set; }
        public string? Data { get; set; }
    }
}
