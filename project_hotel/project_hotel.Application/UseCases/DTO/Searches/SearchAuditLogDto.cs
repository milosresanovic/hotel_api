using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.UseCases.DTO.Searches
{
    public class SearchAuditLogDto : BasePagedSearch
    {
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public bool? IsAuthorized { get; set; }
    }
}
