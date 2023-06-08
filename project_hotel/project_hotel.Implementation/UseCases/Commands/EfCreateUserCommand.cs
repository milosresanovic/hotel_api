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
    public class EfCreateUserCommand : EfUseCase, ICreateUserCommand
    {
        public int Id => 6;

        public string Name => "Create User";

        public string Description => "Creating new user in databse.";

        private readonly CreateUserValidator _validator;

        public EfCreateUserCommand(HotelContext context, CreateUserValidator validator) : base(context)
        {
            _validator = validator;
        }

        public void Execute(CreateUserDto request)
        {
            _validator.ValidateAndThrow(request);

            if(Context.Users.Any(x => x.Username == request.Username))
            {
                throw new ConflictException($"Username {request.Username} is in use.");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Email = request.Email,
            };

            var userUseCases = new List<UserUseCase>();
            var listOfAllowedUseCases = new List<int> {2, 5, 8, 9, 15};
            foreach (var useCase in listOfAllowedUseCases)
            {
                userUseCases.Add(new UserUseCase
                {
                    User = user,
                    UseCaseId = useCase
                });
            }

            if (!string.IsNullOrEmpty(request.ImagePath))
            {
                var image = new Image
                {
                    Path = request.ImagePath
                };
                user.Image = image;
            }

            Context.Users.Add(user);
            Context.UserUceCases.AddRange(userUseCases);

            Context.SaveChanges();
        }
    }
}
