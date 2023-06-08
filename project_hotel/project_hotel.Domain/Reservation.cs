using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Domain
{
    public class Reservation : Entity
    {
        public int UserId { get; set; }
        public int ApartmentId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int GuestsNumber { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual User User { get; set; }
        public virtual Apartment Apartment { get; set; }
    }
}
