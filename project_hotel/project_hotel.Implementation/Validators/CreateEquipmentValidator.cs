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
    public class CreateEquipmentValidator : AbstractValidator<EquipmentDto>
    {
        private HotelContext _context;

        public CreateEquipmentValidator(HotelContext context)
        {
            _context = context;
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Minimum lenght of Name is 3 characters.")
                .MaximumLength(20).WithMessage("Maximum length od Name is 20 characters.")
                .Must(x => context.Equipments.Any(y => y.Name == x)).WithMessage("There is already Equipment with that Name.");
        }
    }
}
