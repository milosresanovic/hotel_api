using Bogus.DataSets;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using project_hotel.Application.Emails;
using project_hotel.Application.Exceptions;
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
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using static Bogus.DataSets.Name;

namespace project_hotel.Implementation.UseCases.Commands
{
    public class EfCreateReservationCommand : EfUseCase, ICreateReservationCommand
    {
        public int Id => 2;

        public string Name => "Create Reservation";

        public string Description => "This will create new reservation n database.";

        private readonly CreateReservationValidator _validator;
        private readonly IEmailSender _emailSender;
        private readonly IApplicationUser _user;

        public EfCreateReservationCommand(HotelContext context, CreateReservationValidator validator, IEmailSender sender, IApplicationUser user) : base(context)
        {
            _validator = validator;
            _emailSender = sender;
            _user = user;
        }

        public void Execute(ReservationDto request)
        {
            if(Context.Apartments.Any(x => x.Id == request.ApartmentId))
            {
                if(request.PersonsNumber > Context.Apartments.Where(x => x.Id == request.ApartmentId).Select(x => x.MaxPersons).First())
                {
                    throw new UnprocessableEntityException("PersonsNumber cant be higher then MaxPersons for selected Apartment.");    
                
                }
            }

            _validator.ValidateAndThrow(request);

            TimeSpan duration = request.DateTo - request.DateFrom;
            int days = duration.Days;

            var apartment = Context.Apartments.Include(x => x.Prices)
                                                      .Where(x => x.Id == request.ApartmentId).FirstOrDefault();

            var pricePerNight = apartment.Prices.Where(x => x.StartDate < request.DateFrom)
                                                .OrderByDescending(x => x.StartDate)
                                                .Select(x => x.Cost).FirstOrDefault();

            decimal totalPrice = days * pricePerNight;

            int userId = _user.Id;
            
            var reservation = new Reservation
            {
                UserId = userId,
                ApartmentId = request.ApartmentId,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                GuestsNumber = request.PersonsNumber,
                TotalPrice = totalPrice
            };

            Context.Reservations.Add(reservation);
            
            Context.SaveChanges();


            var user = Context.Users.FirstOrDefault(x => x.Id == userId);

            _emailSender.Send(new MessageDto
            {
                To = user.Email,
                Title = "Your apartment reservation",
                Body = "<html><head></head>" +
                "<b>Apartment:</b>" + apartment.Name + "<br/>" +
                "<b>User:</b>" + user.FirstName + " " + user.LastName + "<br/>" +
                "<b>Number of guests:</b>" + request.PersonsNumber + "<br/>" +
                "<b>Date start:</b>" + request.DateFrom + "<br/>" +
                "<b>Date end:</b>" + request.DateTo + "<br/>" +
                "<b>Total price: </b>" + totalPrice.ToString() + "<br/>" +
                "<body></body></html>"
            });
        }
    }
}
