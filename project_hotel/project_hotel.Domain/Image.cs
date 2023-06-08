using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Domain
{
    public class Image : Entity
    {
        public string? Path { get; set; }

        public virtual ICollection<ApartmentImage> Apartments { get; set; } = new List<ApartmentImage>();
    }
}
