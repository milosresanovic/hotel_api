using project_hotel.Application.UseCases.DTO;
using project_hotel.Application.UseCases.DTO.Searches;
using project_hotel.Application.UseCases.Queries;
using project_hotel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.UseCases.Queries
{
    public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
    {
       

        public int Id => 13;

        public string Name => "Get users";

        public string Description => "Gets all filtered users from database";
        public EfGetUsersQuery(HotelContext context) : base(context)
        {
        }
        public PagedResponse<UserDto> Execute(SearchUsersDto request)
        {
            var query = Context.Users.AsQueryable();

            if(request.Keyword != null)
            {
                query = query.Where(x => x.Username.Contains(request.Keyword) ||
                                         x.FirstName.Contains(request.Keyword) ||
                                         x.LastName.Contains(request.Keyword) || 
                                         x.Email.Contains(request.Keyword));
            }

            if(request.IsActive != null)
            {
                query = query.Where(x => x.IsActive == request.IsActive);
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

            var response = new PagedResponse<UserDto>();
            response.Data = query.Skip(toSkip).Take(request.PerPage.Value).Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Username = x.Username,
                Email = x.Email,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt
            }).ToList();

            response.TotalCount = query.Count();
            response.CurrentPage = request.Page.Value;
            response.ItemsPerPage = request.PerPage.Value;

            return response;
        }

        
    }
}
