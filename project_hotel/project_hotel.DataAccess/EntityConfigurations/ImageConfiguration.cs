using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.DataAccess.EntityConfigurations
{
    public class ImageConfiguration : EntityConfiguration<Image>
    {
        public override void AddConfiguration(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Path).HasMaxLength(100).IsRequired();

            builder.HasMany(x => x.Apartments)
                    .WithOne(x => x.Image)
                    .HasForeignKey(x => x.ImageId)
                    .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
