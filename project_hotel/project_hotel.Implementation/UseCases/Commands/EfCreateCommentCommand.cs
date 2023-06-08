using FluentValidation;
using project_hotel.Application.UseCases.Commands;
using project_hotel.Application.UseCases.DTO;
using project_hotel.DataAccess;
using project_hotel.Domain;
using project_hotel.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.UseCases.Commands
{
    public class EfCreateCommentCommand : EfUseCase, ICreateCommentCommand
    {
        private readonly CreateCommentValidator _validator;
        private readonly IApplicationUser _user;
        public EfCreateCommentCommand(HotelContext context, CreateCommentValidator validator, IApplicationUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }

        public int Id => 15;

        public string Name => "Create comment";

        public string Description => "Command will create new comment for apartment by user.";

        public void Execute(CreateCommentDto request)
        {
            _validator.ValidateAndThrow(request);

            int userId = _user.Id;

            Context.Comments.Add(new Domain.Comment
            {
                UserId = userId,
                ApartmentId = request.ApartmentId,
                Text = request.Text,
                StarNumber = request.StarNumber
            });

            Context.SaveChanges();
        }
    }
}
