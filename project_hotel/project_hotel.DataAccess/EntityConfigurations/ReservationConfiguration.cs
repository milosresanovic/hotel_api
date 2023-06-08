using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.DataAccess.EntityConfigurations
{
    public class ReservationConfiguration : EntityConfiguration<Reservation>
    {
        public override void AddConfiguration(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.DateFrom).IsRequired();
            builder.Property(x => x.DateTo).IsRequired();
            builder.Property(x => x.GuestsNumber).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired().HasPrecision(10,2);

            
        }
    }
}
