using Microsoft.EntityFrameworkCore;
using project_hotel.Application.Exceptions;
using project_hotel.Application.UseCases.DTO;
using project_hotel.Application.UseCases.Queries;
using project_hotel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.UseCases.Queries
{
    public class EfFindApartmentQuery : EfUseCase, IFindApartmentQuery
    {
        public EfFindApartmentQuery(HotelContext context) : base(context)
        {
        }

        public int Id => 9;

        public string Name => "Find apartment";

        public string Description => "This will find aprtment in databse where is is equal to requested id.";

        public ApartmentDto Execute(int request)
        {
            if(!Context.Apartments.Any(x => x.Id == request))
            {
                throw new EntityNotFoundException("Apartment", request);
            }

            var result = Context.Apartments.Include(x => x.Rooms)
                                          .Include(x => x.Rooms).ThenInclude(x => x.RoomType)
                                          .Include(x => x.Equipments)
                                          .Include(x => x.Equipments).ThenInclude(x => x.Equipment)
                                          .Include(x => x.Images)
                                          .Include(x => x.Images).ThenInclude(x => x.Image)
                                          .Include(x => x.Prices)
                                          .Include(x => x.Category)
                                          .Where(x => !x.DeletedAt.HasValue && x.IsActive)
                                          .Select(x => new ApartmentDto
                                          {
                                              Id = x.Id,
                                              Name = x.Name,
                                              Description = x.Description,
                                              AverageRating = (float)x.Comments.Where(y => y.ApartmentId == x.Id).Average(y => y.StarNumber),
                                              Category = x.Category.Name,
                                              MaxPersons = x.MaxPersons,
                                              Price = x.Prices.Where(y => y.StartDate < DateTime.UtcNow)
                                                              .OrderByDescending(y => y.StartDate)
                                                              .Select(y => y.Cost)
                                                              .FirstOrDefault(),
                                              Images = x.Images.Select(y => y.Image.Path).ToList(),
                                              Equipments = x.Equipments.Select(y => new EquipmentDto
                                              {
                                                  Id = y.EquipmentId,
                                                  Name = y.Equipment.Name
                                              }).ToList(),
                                              Rooms = x.Rooms.Select(y => new RoomDto
                                              {
                                                  Id = y.RoomType.Id,
                                                  Name = y.RoomType.Name,
                                                  Area = y.Area
                                              }).ToList(),
                                              Comments = x.Comments.Select(y => new CommentDto
                                              {
                                                  Id = y.Id,
                                                  User = y.User.Username,
                                                  Text = y.Text,
                                                  StarNumber = y.StarNumber,
                                                  Date = y.CreatedAt
                                              })
                                          })
                                          .FirstOrDefault(x => x.Id == request);


            return result;
        }
    }
}
