using FluentValidation;
using project_hotel.Application.UseCases.DTO;
using project_hotel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.Validators
{
    public class CreatePriceValidator : AbstractValidator<CreatePriceDto>
    {
        public HotelContext Context { get; set; }
        public CreatePriceValidator(HotelContext context)
        {
            Context = context;

            RuleFor(x => x.ApartmentId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Yout must eneter apartmentId.")
                .Must(x => Context.Apartments.Any(y => y.Id == x)).WithMessage("There is no apartment with given Id.");

            RuleFor(x => x.StartDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("DateStart is required field.")
                .Must(x => x > DateTime.UtcNow).WithMessage("StartDate must be in future.")
                .Must(x => x < DateTime.UtcNow.AddMonths(12)).WithMessage("You can add price only 1 year in future.");

            RuleFor(x => x.Cost)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must enter Cost of apartment.")
                .Must(x => x > 0).WithMessage("Cost of apartment cant be lover then 0.")
                .Must(x => x < 10000).WithMessage("Cost cant be more then 10000.");
        }
    }
}
