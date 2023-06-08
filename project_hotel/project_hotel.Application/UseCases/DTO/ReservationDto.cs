using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.UseCases.DTO
{
    public class ReservationDto 
    {
        public int ApartmentId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int PersonsNumber { get; set; }
    }

    public class ReservationResponseDto : BaseDto
    {
        public string Username { get; set; }
        public string Apartment { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal Cost { get; set; }
        public int PersonsNumber { get; set; }
    }

}
