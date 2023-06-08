using FluentValidation;
using project_hotel.Application.UseCases.DTO;
using project_hotel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.Validators
{
    public class CreateReservationValidator : AbstractValidator<ReservationDto>
    {
        private HotelContext _context;
        public CreateReservationValidator(HotelContext context)
        {
            _context = context;


            RuleFor(x => x.ApartmentId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("ApartmentId is required field.")
                .Must(x => _context.Apartments.Any(y => y.Id == x)).WithMessage("There is no Apartment with that ID.");

            RuleFor(x => x.DateFrom)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("DateFrom is required field.")
                .Must(x => x > DateTime.UtcNow).WithMessage("Field DateFrom must be in future.");

            RuleFor(x => x.DateTo)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("DateTo is required field.")
                .Must(x => x > DateTime.UtcNow).WithMessage("Field DateTo must be in future.")
                .GreaterThan(x => x.DateFrom).WithMessage("Field DateTo must be higher then field DateFrom.");


            
            RuleFor(x => x)
                .Must(x => !context.Reservations.Where(y => y.ApartmentId == x.ApartmentId)
                                                .Any(y => (x.DateFrom >= y.DateFrom && x.DateFrom <= y.DateTo) ||
                                                    (x.DateTo >= y.DateFrom && x.DateTo <= y.DateTo) ||
                                                    (x.DateFrom <= y.DateFrom && x.DateTo >= y.DateTo)))
                .WithMessage("Apartment is not free in that period.");
        }
    }
}
