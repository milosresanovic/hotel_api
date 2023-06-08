using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Domain
{
    public class RoomType : Entity
    {
        public string? Name { get; set; }

        public virtual ICollection<ApartmentRoom> Apartments { get; set; } = new List<ApartmentRoom>();
    }
}
