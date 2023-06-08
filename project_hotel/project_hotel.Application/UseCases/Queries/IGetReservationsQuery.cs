using project_hotel.Application.UseCases.DTO;
using project_hotel.Application.UseCases.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Application.UseCases.Queries
{
    public interface IGetReservationsQuery : IQuery<SearchReservationsDto, PagedResponse<ReservationResponseDto>>
    {
    }
}
