using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.DataAccess.EntityConfigurations
{
    public class EquipmentConfiguration : EntityConfiguration<Equipment>
    {
        public override void AddConfiguration(EntityTypeBuilder<Equipment> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Apartments)
                   .WithOne(x => x.Equipment)
                   .HasForeignKey(x => x.EquipmentId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
