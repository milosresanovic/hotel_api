using FluentValidation;
using Microsoft.EntityFrameworkCore;
using project_hotel.Application.UseCases.DTO;
using project_hotel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        private HotelContext _context;
        public UpdateUserValidator(HotelContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("UserId is required fiel.")
                .Must(x => _context.Users.Any(y => y.Id == x)).WithMessage("There is no user with specific Id.");

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username is required field.")
                .MinimumLength(5).WithMessage("Min characher for Username is 5")
                .MaximumLength(25).WithMessage("Max characters for Username is 25")
                .Matches("^[a-z0-9_]+$").WithMessage("Only letters, numbers and underscore are aloowed for Username");

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("FirstName is required field.")
                .MinimumLength(2).WithMessage("Min characher for FirstName is 2")
                .MaximumLength(25).WithMessage("Max characters for FirstName is 25")
                .Matches("^[A-Za-z]+$").WithMessage("Only letters, first letter uppercase.");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("LastName is required field.")
                .MinimumLength(2).WithMessage("Min characher for LastName is 2")
                .MaximumLength(25).WithMessage("Max characters for LastName is 25")
                .Matches("^[A-Za-z]+$").WithMessage("Only letters, first letter uppercase.");
        }
    }
}
