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
using System.Transactions;

namespace project_hotel.Implementation.UseCases.Commands
{
    public class EfUpdateApartmentCommand : EfUseCase, IUpdateApartmentCommand
    {
        public EfUpdateApartmentCommand(HotelContext context, UpdateApartmentValidator validator) : base(context)
        {
            _validator = validator;
        }

        private readonly UpdateApartmentValidator _validator;

        public int Id => 10;

        public string Name => "Update apartment";

        public string Description => "This will update apartment and store values in database.";

        public void Execute(UpdateApartmentDto request)
        {
            var apartment = Context.Apartments.FirstOrDefault(x => x.Id == request.Id);

            if(apartment == null)
            {
                throw new EntityNotFoundException("Apartment", request.Id);
            }
    
            _validator.ValidateAndThrow(request);

            if(request.Name != apartment.Name)
            {
                var apartmentWithReqestedName = Context.Apartments.FirstOrDefault(x => x.Name == request.Name);

                if(apartmentWithReqestedName != null && apartmentWithReqestedName.Id != apartment.Id)
                {
                    throw new UnprocessableEntityException("There is already apartman with that name.");
                }
            }

            var oldRooms = Context.ApartmentRooms.Where(x => x.ApartmentId == request.Id).ToList();
            var oldEquipment = Context.ApartmentEquipments.Where(x => x.ApartmentId == request.Id).ToList();
            var oldImages = Context.ApartmentImages.Where(x => x.ApartmentId == request.Id).ToList();

            Context.ApartmentRooms.RemoveRange(oldRooms);
            Context.ApartmentEquipments.RemoveRange(oldEquipment);
            Context.ApartmentImages.RemoveRange(oldImages);


            if (apartment.Prices.Where(x => x.ApartmentId == request.Id).OrderByDescending(x => x.StartDate).Select(x => x.Cost).FirstOrDefault() != request.Price)
            {
                var price = new Price
                {
                    ApartmentId = request.Id,
                    Cost = request.Price,
                    StartDate = DateTime.UtcNow
                };

                apartment.Prices.Add(price);
            }

            var apartmentRooms = new List<ApartmentRoom>();
            foreach (var r in request.Rooms)
            {
                apartmentRooms.Add(new ApartmentRoom
                {
                    ApartmentId = request.Id,
                    RoomTypeId = r.RoomId,
                    Area = r.Area
                });
            }

            var apartmentEquipments = new List<ApartmentEquipment>();
            foreach (var e in request.Equipments)
            {
                apartmentEquipments.Add(new ApartmentEquipment
                {
                    ApartmentId = request.Id,
                    EquipmentId = e
                });
            }

            foreach (var o in oldImages)
            {
                var image = Context.Images.FirstOrDefault(x => x.Id == o.ImageId);

                if (image != null)
                {
                    Context.Images.Remove(image);
                }
            }

            var newImages = new List<Image>();

            if (request.ImagePaths != null)
            {
                foreach (var i in request.ImagePaths)
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
            foreach (var i in newImages)
            {
                newApartmentImages.Add(new ApartmentImage
                {
                    Apartment = apartment,
                    Image = i
                });
            }

            apartment.Name = request.Name;
            apartment.Description = request.Description;
            apartment.MaxPersons = request.MaxPersons;
            apartment.CategoryId = request.CategoryId;
            apartment.Rooms = apartmentRooms;
            apartment.Equipments = apartmentEquipments;
            apartment.Images = newApartmentImages;

            Context.SaveChanges();
        }
    }
}
