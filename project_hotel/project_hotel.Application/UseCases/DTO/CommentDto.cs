using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.UseCases.DTO
{
    public class CommentDto : BaseDto
    {
        public string User { get; set; }
        public string Text { get; set; }
        public int StarNumber { get; set; }
        public DateTime Date { get; set; }
    }

    public class CreateCommentDto
    {
        public int ApartmentId { get; set; }
        public string Text { get; set; }
        public int StarNumber { get; set; }

    }
}
