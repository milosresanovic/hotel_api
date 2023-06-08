using FluentValidation;
using project_hotel.Application.Exceptions;
using project_hotel.Application.UseCases.Commands;
using project_hotel.Application.UseCases.DTO;
using project_hotel.DataAccess;
using project_hotel.Domain;
using project_hotel.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.UseCases.Commands
{
    public class EfUpdateUserCommand : EfUseCase, IUpdateUserCommand
    {
     

        public int Id => 14;

        public string Name => "Update user";

        public string Description => "Updates user in database";

        private readonly UpdateUserValidator _validator;
        public EfUpdateUserCommand(HotelContext context, UpdateUserValidator validator) : base(context)
        {
            _validator = validator;
        }
        public void Execute(UpdateUserDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = Context.Users.FirstOrDefault(x => x.Id == request.Id && x.DeletedAt == null);

            if(user == null)
            {
                throw new EntityNotFoundException("User", request.Id);
            }

            if(Context.Users.Any(x => x.Username == request.Username && x.Id != request.Id))
            {
                throw new ConflictException("Username is already in use.");
            }

            if(request.ImagePath != null)
            {
                var image = new Image
                {
                    Path = request.ImagePath
                };

                Context.Images.Add(new Domain.Image
                {
                    Path = request.ImagePath,
                });

                user.Image = image;
            }

            user.Username = request.Username;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
           
            Context.SaveChanges();
        }
    }
}
