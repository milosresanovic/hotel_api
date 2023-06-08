using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.UseCases.DTO
{
    public class ApartmentDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float? AverageRating { get; set; }
        public int MaxPersons { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public IEnumerable<RoomDto> Rooms { get; set; }
        public IEnumerable<EquipmentDto> Equipments { get; set; }
        public IEnumerable<string> Images { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }

    }

    public class CreateApartmentDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxPersons { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<int> Equipments { get; set; }
        public IEnumerable<CreateRoomDto> Rooms { get; set; }
        public ICollection<string>? ImagePaths { get; set; } = new List<string>();
    }

    public class UpdateApartmentDto : CreateApartmentDto
    {
        public int Id { get; set; }
    }
}
