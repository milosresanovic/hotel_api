using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.UseCases.DTO.Searches
{
    public class SearchUsersDto : BasePagedSearch
    {
        public bool? IsActive { get; set; }
    }
}
