using Microsoft.EntityFrameworkCore;
using project_hotel.Application.UseCases.DTO;
using project_hotel.Application.UseCases.DTO.Searches;
using project_hotel.Application.UseCases.Queries;
using project_hotel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.UseCases.Queries
{
    public class EfGetReservationsQuery : EfUseCase, IGetReservationsQuery
    {
        public EfGetReservationsQuery(HotelContext context) : base(context)
        {
        }

        public int Id => 17;

        public string Name => "Get reservations";

        public string Description => "Returns filtered reservations by keyword and date";

        public PagedResponse<ReservationResponseDto> Execute(SearchReservationsDto request)
        {
            var query = Context.Reservations.Include(x => x.User)
                                                   .Include(x => x.Apartment)
                                                   .AsQueryable();

            if(request.Keyword != null)
            {
                query = query.Where(x => x.Apartment.Name.Contains(request.Keyword) ||
                                 x.User.Username.Contains(request.Keyword) ||
                                 x.User.FirstName.Contains(request.Keyword) ||
                                 x.User.LastName.Contains(request.Keyword));
            }

            if(request.DateFrom != null)
            {
                query = query.Where(x => x.DateFrom >= request.DateFrom);
            }

            if(request.DateTo != null)
            {
                query = query.Where(x => x.DateTo <= request.DateTo);
            }

            if(request.PriceFrom != null)
            {
                query = query.Where(x => x.TotalPrice >= request.PriceFrom);
            }

            if(request.PriceTo != null)
            {
                query = query.Where(x => x.TotalPrice <= request.PriceTo);
            }

            if(request.PersonsNumberFrom != null)
            {
                query = query.Where(x => x.GuestsNumber >= request.PersonsNumberFrom);
            }

            if(request.PersonsNumberTo != null)
            {
                query = query.Where(x => x.GuestsNumber <= request.PersonsNumberTo);
            }

            if (request.PerPage == null || request.PerPage < 1)
            {
                request.PerPage = 15;
            }

            if (request.Page == null || request.Page < 1)
            {
                request.Page = 1;
            }

            var toSkip = (request.Page.Value - 1) * request.PerPage.Value;

            var response = new PagedResponse<ReservationResponseDto>();
            response.TotalCount = query.Count();

            response.Data = query.Skip(toSkip).Take(request.PerPage.Value).Select(x => new ReservationResponseDto
            {
                Id = x.Id,
                Username = x.User.Username,
                Apartment = x.Apartment.Name,
                DateFrom = x.DateFrom,
                DateTo = x.DateTo,
                Cost = x.TotalPrice,
                PersonsNumber = x.GuestsNumber
            });

            response.CurrentPage = request.Page.Value;
            response.ItemsPerPage = request.PerPage.Value;

            return response;
        }
    }
}
