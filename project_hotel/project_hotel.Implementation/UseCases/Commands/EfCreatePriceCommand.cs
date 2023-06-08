using FluentValidation;
using project_hotel.Application.UseCases.Commands;
using project_hotel.Application.UseCases.DTO;
using project_hotel.DataAccess;
using project_hotel.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.UseCases.Commands
{
    public class EfCreatePriceCommand : EfUseCase, ICreatePriceCommand
    {
        private readonly CreatePriceValidator _validator;
        public EfCreatePriceCommand(HotelContext context, CreatePriceValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 16;

        public string Name => "Create price";

        public string Description => "This will create new price for apartment";

        public void Execute(CreatePriceDto request)
        {
            _validator.ValidateAndThrow(request);

            Context.Prices.Add(new Domain.Price
            {
                StartDate = request.StartDate,
                ApartmentId = request.ApartmentId,
                Cost = request.Cost
            });

            Context.SaveChanges();
        }
    }
}
