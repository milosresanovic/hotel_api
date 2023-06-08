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
    public class CreateCommentValidator : AbstractValidator<CreateCommentDto>
    {
        private HotelContext Context { get; set; }
        public CreateCommentValidator(HotelContext context)
        {
            Context = context;

            RuleFor(x => x.Text)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must enter the text of comment.")
                .MinimumLength(3).WithMessage("Minimum length is 3 characters.")
                .MaximumLength(250).WithMessage("Maximum length is 250 characters.");

            RuleFor(x => x.StarNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must enter the star number for room you comment.")
                .Must(x => x > 0 && x < 6).WithMessage("Allowed options for starNumber are 1 - 5.");

            

            RuleFor(x => x.ApartmentId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("ApartmentId is required field.")
                .Must(x => Context.Apartments.Any(y => y.Id == x)).WithMessage($"There is no user with given ID");

        }
    }
}
