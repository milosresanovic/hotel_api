using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.UseCases.DTO.Searches
{
    public class SearchApartmentsDto : BasePagedSearch
    {
        public float? AverageRating { get; set; }
        public int? MaxPersons { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public IEnumerable<int>? RoomTypeIds { get; set; }
    }
}
