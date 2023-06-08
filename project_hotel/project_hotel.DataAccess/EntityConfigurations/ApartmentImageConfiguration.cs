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
    public class ApartmentImageConfiguration : IEntityTypeConfiguration<ApartmentImage>
    {
        public void Configure(EntityTypeBuilder<ApartmentImage> builder)
        {
            builder.HasKey(x => new { x.ApartmentId, x.ImageId });
        }
    }
}
