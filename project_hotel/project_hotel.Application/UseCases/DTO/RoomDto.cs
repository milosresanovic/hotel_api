using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.UseCases.DTO
{
    public class RoomDto : BaseDto
    {
        public string Name { get; set; }
        public int Area { get; set; }
    }

    public class CreateRoomDto
    {
        public int RoomId { get; set; }
        public int Area { get; set; }
    }
}
