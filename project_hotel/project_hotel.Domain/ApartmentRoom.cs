using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Domain
{
    public class ApartmentRoom
    {
        public int ApartmentId { get; set; }
        public int RoomTypeId { get; set; }
        public int Area { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual RoomType RoomType { get; set; }
    }
}
