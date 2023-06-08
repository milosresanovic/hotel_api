using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Domain
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? UseCase { get; set; }
        public DateTime Time { get; set; }
        public bool IsAuthorized { get; set; }
        public string? Data { get; set; }

        public virtual User User { get; set; }

    }
}
