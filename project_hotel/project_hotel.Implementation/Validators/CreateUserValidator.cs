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
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        private readonly HotelContext _context;

        public CreateUserValidator(HotelContext context)
        {
            _context = context;

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username is required field.")
                .MinimumLength(5).WithMessage("Min characher for Username is 5")
                .MaximumLength(25).WithMessage("Max characters for Username is 25")
                .Matches("^[a-z0-9_]+$").WithMessage("Only letters, numbers and underscore are aloowed for Username")
                .Must(x => !_context.Users.Any(y => y.Username == x)).WithMessage("Username is in use.");

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

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required field.")
                 .Must(x => !_context.Users.Any(y => y.Email == x)).WithMessage("Email is in use.")
                .Matches("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$").WithMessage("Email in incorrect format.");
               

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password is required field.")
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z0-9\\s]).{8,}$").WithMessage("At least 8 characters long, Contains at least one uppercase letter, Contains at least one lowercase letter, Contains at least one digit, At least 1 special character, No white spaces.");

        }
    }
}
