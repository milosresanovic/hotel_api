using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Domain
{
    public class Apartment : Entity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float? AverageRating { get; set; }
        public int CategoryId { get; set; }
        public int MaxPersons { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ApartmentEquipment> Equipments { get; set; } = new List<ApartmentEquipment>();
        public virtual ICollection<ApartmentRoom> Rooms { get; set; } = new List<ApartmentRoom>();
        public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
        public virtual ICollection<ApartmentImage> Images { get; set; } = new List<ApartmentImage>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
