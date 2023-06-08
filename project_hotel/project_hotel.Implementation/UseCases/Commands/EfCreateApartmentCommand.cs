using FluentValidation;
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
    public class EfCreateApartmentCommand : EfUseCase, ICreateApartmentCommand
    {
        public int Id => 5;

        public string Name => "Create apartment";

        public string Description => "Create new apartment and add it in database";

        private readonly CreateApartmentValidator Validator;
        public EfCreateApartmentCommand(HotelContext context, CreateApartmentValidator validator)
            :base(context)
        {
            Validator = validator;
        }
        public void Execute(CreateApartmentDto request)
        {
            Validator.ValidateAndThrow(request);

            var apartment = new Apartment
            {
                Name = request.Name,
                Description = request.Description,
                MaxPersons = request.MaxPersons,
            };

            var apartmentRooms = new List<ApartmentRoom>();
            foreach (var r in request.Rooms)
            {
                apartmentRooms.Add(new ApartmentRoom
                {
                    Apartment = apartment,
                    RoomTypeId = r.RoomId,
                    Area = r.Area
                });
            }

            var price = new Price
            {
                Apartment = apartment,
                Cost = request.Price,
                StartDate = DateTime.UtcNow
            };

            var apartmentEquipments = new List<ApartmentEquipment>();
            foreach (var e in request.Equipments)
            {
                apartmentEquipments.Add(new ApartmentEquipment
                {
                    Apartment = apartment,
                    EquipmentId = e
                });
            }

            apartment.CategoryId = request.CategoryId;
            apartment.Rooms = apartmentRooms;
            apartment.Prices = new List<Price>
            {
                new Price
                {
                    Apartment = apartment,
                    Cost = request.Price,
                    StartDate = DateTime.UtcNow
                }
            };
            apartment.Equipments = apartmentEquipments;



            var newImages = new List<Image>();

            if (request.ImagePaths != null)
            {
                foreach(var i in request.ImagePaths)
                {
                    var image = new Image
                    {
                        Path = i
                    };
                    newImages.Add(image);
                }

            }

            Context.Images.AddRange(newImages);

            var newApartmentImages = new List<ApartmentImage>();
            foreach(var i in newImages)
            {
                apartment.Images.Add(new ApartmentImage
                {
                    Apartment = apartment,
                    Image = i
                });
            }

            Context.Apartments.Add(apartment);

            Context.SaveChanges();
        }
    }
}
