using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.DataAccess.EntityConfigurations
{
    public class ApartmentConfiguration : EntityConfiguration<Apartment>
    {
        public override void AddConfiguration(EntityTypeBuilder<Apartment> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(700).IsRequired();
            builder.Property(x => x.AverageRating).IsRequired(false);

            builder.HasMany(x => x.Equipments)
                   .WithOne(x => x.Apartment)
                   .HasForeignKey(x => x.ApartmentId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasMany(x => x.Rooms)
                   .WithOne(x => x.Apartment)
                   .HasForeignKey(x => x.ApartmentId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasMany(x => x.Prices)
                   .WithOne(x => x.Apartment)
                   .HasForeignKey(x => x.ApartmentId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasMany(x => x.Images)
                   .WithOne(x => x.Apartment)
                   .HasForeignKey(x => x.ApartmentId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasMany(x => x.Comments)
                   .WithOne(x => x.Apartment)
                   .HasForeignKey(x => x.ApartmentId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasMany(x => x.Reservations)
                   .WithOne(x => x.Apartment)
                   .HasForeignKey(x => x.ApartmentId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
