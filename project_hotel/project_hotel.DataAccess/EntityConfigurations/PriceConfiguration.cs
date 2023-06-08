using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.DataAccess.EntityConfigurations
{
    public class PriceConfiguration : EntityConfiguration<Price>
    {
        public override void AddConfiguration(EntityTypeBuilder<Price> builder)
        {
            builder.Property(x => x.Cost).HasPrecision(8, 2);
            builder.Property(x => x.StartDate).IsRequired();
        }
    }
}
