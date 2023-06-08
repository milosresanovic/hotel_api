using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.DataAccess.EntityConfigurations
{
    public class RoomTypeConfiguration : EntityConfiguration<RoomType>
    {
        public override void AddConfiguration(EntityTypeBuilder<RoomType> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(x => x.Apartments)
                   .WithOne(x => x.RoomType)
                   .HasForeignKey(x => x.RoomTypeId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
