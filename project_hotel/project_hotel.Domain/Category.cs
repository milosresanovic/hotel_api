using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Domain
{
    public class Category : Entity
    {
        public string? Name { get; set; }
        public int? ParentId { get; set; }

        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; } = new List<Category>();
    }
}
