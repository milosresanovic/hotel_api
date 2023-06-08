using project_hotel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.Implementation.UseCases
{
    public class EfUseCase
    {
        protected HotelContext Context { get; }
        protected EfUseCase(HotelContext context)
        {
            Context = context;
        }
    }
}
