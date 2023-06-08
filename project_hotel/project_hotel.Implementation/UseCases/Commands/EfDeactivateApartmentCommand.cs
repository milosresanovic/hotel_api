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
    public class EfDeactivateApartmentCommand : EfUseCase, IDeactivateApartmentCommand
    {
        public EfDeactivateApartmentCommand(HotelContext context) : base(context)
        {
        }

        public int Id => 11;

        public string Name => "Deactivate apartmetn";

        public string Description => "This will deactivate apartment and then users cant make new reservations.";

        public void Execute(int request)
        {
            var apartment = Context.Apartments.FirstOrDefault(x => x.Id == request);


            if(apartment == null)
            {
                throw new EntityNotFoundException("Apartment", request);
            }

            if (!apartment.IsActive)
            {
                throw new ConflictException("Apartment is already deactivated.");
            }

            apartment.IsActive = false;

            Context.SaveChanges();
        }
    }
}
