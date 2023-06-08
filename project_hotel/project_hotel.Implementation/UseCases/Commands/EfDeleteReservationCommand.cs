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
    public class EfDeleteReservationCommand : EfUseCase, IDeleteReservationCommand
    {
        public int Id => 4;

        public string Name => "Delete Reservation";

        public string Description => "This command will set DeletedAt if DateFrom is higer then current date.";

        public EfDeleteReservationCommand(HotelContext context, IApplicationUser user) : base(context)
        {
            User = user;
        }
        public IApplicationUser User { get; }

        public void Execute(int request)
        {
            var reservation = Context.Reservations.FirstOrDefault(x => x.Id == request && x.DeletedAt == null);

            if(reservation == null)
            {
                throw new EntityNotFoundException("Reservation", request);
            }

            if(reservation.DateFrom < DateTime.UtcNow)
            {
                throw new UnprocessableEntityException("Cant delete reservation because it is already started.");
            }

            reservation.DeletedAt = DateTime.UtcNow;
            reservation.DeletedBy = User?.Username;

            Context.SaveChanges();
        }
    }
}
