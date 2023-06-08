using Bogus;
using Microsoft.EntityFrameworkCore;
using project_hotel.Application.UseCases;
using project_hotel.Application.UseCases.DTO;
using project_hotel.Application.UseCases.DTO.Searches;
using project_hotel.Application.UseCases.Queries;
using project_hotel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.UseCases.Queries
{
    public class EfGetApartmentsQuery : EfUseCase, IGetApartmentsQuery
    {
        public int Id => 8;

        public string Name => "Apartments search";

        public string Description => "Search all available apartments in database.";

        public EfGetApartmentsQuery(HotelContext context) : base(context)
        {

        }

        public PagedResponse<ApartmentDto> Execute(SearchApartmentsDto request)
        {
            var query = Context.Apartments.Include(x => x.Rooms)
                                          .Include(x => x.Rooms).ThenInclude(x => x.RoomType)
                                          .Include(x => x.Equipments)
                                          .Include(x => x.Equipments).ThenInclude(x => x.Equipment)
                                          .Include(x => x.Images)
                                          .Include(x => x.Images).ThenInclude(x => x.Image)
                                          .Include(x => x.Prices)
                                          .Include(x => x.Category)
                                          .Where(x => !x.DeletedAt.HasValue && x.IsActive)
                                          .AsQueryable();

            if(request.Keyword != null)
            {
                query = query.Where(x => x.Name.Contains(request.Keyword) ||
                                        (x.Category.Name.Contains(request.Keyword)));
            }
            if(request.MinPrice != null)
            {
                query = query.Where(x => x.Prices.Where(y => y.StartDate < DateTime.UtcNow)
                                                 .OrderBy(x => x.StartDate)
                                                 .Last().Cost > request.MinPrice);
            }

            if(request.MaxPrice != null)
            {
                query = query.Where(x => x.Prices.Where(y => y.StartDate < DateTime.UtcNow)
                                                 .OrderBy(x => x.StartDate)
                                                 .Last().Cost < request.MaxPrice);
            }

            if(request.MaxPersons != null)
            {
                query = query.Where(x => x.MaxPersons >= request.MaxPersons);
            }

            if(request.AverageRating != null)
            {
                query = query.Where(x => x.Comments.Average(y => y.StarNumber) >= request.AverageRating);
            }

            if (request.RoomTypeIds != null)
            {
                query = query.Where(x => x.Rooms.All(y => request.RoomTypeIds.Contains(y.RoomTypeId)));
            }

            if (request.PerPage == null || request.PerPage<1)
            {
                request.PerPage = 15;
            }

            if(request.Page == null || request.Page < 1)
            {
                request.Page = 1;
            }

            var toSkip = (request.Page.Value - 1) * request.PerPage.Value;

            var response = new PagedResponse<ApartmentDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(request.PerPage.Value).Select(x => new ApartmentDto
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
            }).ToList();

            response.CurrentPage = request.Page.Value;
            response.ItemsPerPage = request.PerPage.Value;

            return response;
        }
    }
}
