using Microsoft.EntityFrameworkCore;
using project_hotel.Application.Exceptions;
using project_hotel.Application.UseCases.Commands;
using project_hotel.DataAccess;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.UseCases.Commands
{
    public class EfDeleteUserCommand : EfUseCase, IDeleteUserCommand
    {
        public EfDeleteUserCommand(HotelContext context, IApplicationUser user) : base(context)
        {
        }
        public IApplicationUser User { get; }

        public int Id => 12;

        public string Name => "Delete user";

        public string Description => "This will set IsDeleted on true in databse.";

        public void Execute(int request)
        {
            var user = Context.Users.Include(x => x.Reservations).FirstOrDefault(x => x.Id == request);

            if(user == null)
            {
                throw new EntityNotFoundException("User", request);
            }

            var doesUserHaveFutureReservations = user.Reservations.Any(x => x.DateFrom > DateTime.UtcNow);

            if (doesUserHaveFutureReservations)
            {
                throw new ConflictException($"User cant be deleted because User with identifier {request} has one or more reservations in future.");
            }

            user.IsActive = false;
            user.DeletedBy = User?.Username;

            Context.SaveChanges();
        }
    }
}
