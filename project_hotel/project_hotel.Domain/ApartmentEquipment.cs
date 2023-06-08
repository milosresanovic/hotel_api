using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Domain
{
    public class ApartmentEquipment
    {
        public int ApartmentId { get; set; }
        public int EquipmentId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
