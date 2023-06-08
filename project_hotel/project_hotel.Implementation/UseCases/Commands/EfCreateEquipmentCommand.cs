using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
    public class EfCreateEquipmentCommand : EfUseCase, ICreateEquipmentCommand
    {
        private CreateEquipmentValidator _validator;
        public int Id => 3;

        public string Name => "Create Equipment (EntityFramework Core)";

        public string Description => "Command will create new Equipment in database";

        public EfCreateEquipmentCommand(HotelContext context, CreateEquipmentValidator validator) : base(context)
        {
            _validator = validator;
        }

        public void Execute(EquipmentDto request)
        {
            _validator.ValidateAndThrow(request);

            var equipment = new Equipment
            {
                Name = request.Name
            };

            Context.Add(equipment);

            Context.SaveChanges();
        }
    }
}
