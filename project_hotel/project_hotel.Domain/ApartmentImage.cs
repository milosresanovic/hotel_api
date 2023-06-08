using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Domain
{
    public class ApartmentImage
    {
        public int ApartmentId { get; set; }
        public int ImageId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Image Image { get; set; }
    }
}
