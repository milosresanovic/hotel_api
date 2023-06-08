using project_hotel.Application.Exceptions;
using project_hotel.Application.UseCases.Commands;
using project_hotel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.UseCases.Commands
{
    public class EfDeleteCommentCommand : EfUseCase, IDeleteCommentCommand
    {
        public EfDeleteCommentCommand(HotelContext context) : base(context)
        {
        }

        public int Id => 18;

        public string Name => "Delete comment";

        public string Description => "Command will set IsActive to false and DeletedAt to Utc.Now";

        public void Execute(int request)
        {
            var comment = Context.Comments.FirstOrDefault(x => x.Id == request);

            if(comment == null)
            {
                throw new EntityNotFoundException("Comment", request);
            }

            comment.IsActive = false;
            comment.DeletedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
