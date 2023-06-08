using Newtonsoft.Json;
using project_hotel.Application.UseCases.DTO;
using project_hotel.Application.UseCases.DTO.Searches;
using project_hotel.Application.UseCases.Queries;
using project_hotel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace project_hotel.Implementation.UseCases.Queries
{
    public class EfGetAuditLogsQuery : EfUseCase, IGetAuditLogsQuery
    {
        public int Id => 7;

        public string Name => "Get audit logs";

        public string Description => "Return all logs from auditLogs table in JSON format.";

        public EfGetAuditLogsQuery(HotelContext context) : base(context)
        {

        }

        public PagedResponse<AuditLogDto> Execute(SearchAuditLogDto search)
        {
            var query = Context.AuditLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.UseCase.Contains(search.Keyword) || x.Username.Contains(search.Keyword));
            }

            if(search.TimeStart != null)
            {
                query = query.Where(x => x.Time > search.TimeStart);
            }

            if (search.TimeEnd != null)
            {
                query = query.Where(x => x.Time < search.TimeEnd);
            }

            if(search.IsAuthorized != null)
            {
                if (search.IsAuthorized.Value)
                {
                    query = query.Where(x => x.IsAuthorized);
                }
                else
                {
                    query = query.Where(x => !x.IsAuthorized);
                }
            }

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<AuditLogDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new AuditLogDto
            {
                Username = x.Username,
                UseCase = x.UseCase,
                Time = x.Time,
                IsAuthorized = x.IsAuthorized ? "Authorizes" : "Not Authorized",
                Data = x.Data
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
