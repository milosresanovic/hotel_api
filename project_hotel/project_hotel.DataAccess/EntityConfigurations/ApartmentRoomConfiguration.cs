using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.DataAccess.EntityConfigurations
{
    public class ApartmentRoomConfiguration : IEntityTypeConfiguration<ApartmentRoom>
    {
        public void Configure(EntityTypeBuilder<ApartmentRoom> builder)
        {
            builder.HasKey(x => new { x.ApartmentId, x.RoomTypeId });
        }
    }
}
