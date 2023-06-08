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
    public class UpdateApartmentValidator : AbstractValidator<UpdateApartmentDto>
    {
        private HotelContext _context;
        public UpdateApartmentValidator(HotelContext context)
        {
            _context = context;

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Description is required field.")
                .MinimumLength(3).WithMessage("Minimum number of characters for Description is 10")
                .MaximumLength(500).WithMessage("Maximum number of characters for Description is 500");

            RuleFor(x => x.MaxPersons)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must insert a MaxPersons for apartment.")
                .Must(x => x < 1 ? false : true).WithMessage("MaxPersons cant be lower then 1.")
                .Must(x => x > 7 ? false : true).WithMessage("MaxPersons cant be greater then 7");

            RuleFor(x => x.CategoryId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("CategoryId is required field.")
                .Must(x => _context.Categories.Any(y => y.Id == x)).WithMessage("That category doesnt exists in database.");

            RuleForEach(x => x.Rooms)
                .Cascade(CascadeMode.Stop)
                .Must(x => _context.RoomTypes.Any(y => y.Id == x.RoomId)).WithMessage("Some of Rooms you selected does not exists.");

            RuleFor(x => x.Price)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Price is required filed for apartment")
                .Must(x => x > 1).WithMessage("Price cant be lower then 1.")
                .Must(x => x < 100000).WithMessage("Price cant be greater then 100000.");
        }
    }
}
