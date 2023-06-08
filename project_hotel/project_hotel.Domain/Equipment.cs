using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Domain
{
    public class Equipment : Entity
    {
        public string? Name { get; set; }

        public virtual ICollection<ApartmentEquipment> Apartments { get; set; } = new List<ApartmentEquipment>();
    }
}
