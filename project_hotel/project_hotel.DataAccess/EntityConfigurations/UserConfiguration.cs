using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.DataAccess.EntityConfigurations
{
    public class UserConfiguration : EntityConfiguration<User>
    {
        public override void AddConfiguration(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Username).HasMaxLength(50).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(250).IsRequired();
            //builder.Property(x => x.ImagePath).HasMaxLength(150).IsRequired(false);

            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.FirstName);
            builder.HasIndex(x => x.LastName);

            builder.HasMany(x => x.Reservations)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasMany(x => x.UseCases)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.AuditLogs)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
