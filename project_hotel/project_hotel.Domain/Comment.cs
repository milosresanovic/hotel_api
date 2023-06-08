using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Domain
{
    public class Comment : Entity
    {
        public int UserId { get; set; }
        public int ApartmentId { get; set; }
        public string? Text { get; set; }
        public int StarNumber { get; set; }

        public virtual User User { get; set; }
        public virtual Apartment Apartment { get; set; } 

    }
}
