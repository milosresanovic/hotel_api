using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Domain
{
    public class Price : Entity
    {
        public int ApartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Cost { get; set; }

        public virtual Apartment Apartment { get; set; }
    }
}
