using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.UseCases.DTO
{
    public class PriceDto : BaseDto
    {
        public int ApartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Cost { get; set; }
    }

    public class CreatePriceDto
    {
        public int ApartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Cost { get; set; }
    }
}
