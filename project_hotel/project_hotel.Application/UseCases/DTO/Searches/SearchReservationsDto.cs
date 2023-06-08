using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.UseCases.DTO.Searches
{
    public class SearchReservationsDto : BasePagedSearch
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public int? PersonsNumberFrom { get; set; }
        public int? PersonsNumberTo { get; set; }
    }
}
