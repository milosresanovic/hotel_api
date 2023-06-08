using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.DataAccess.EntityConfigurations
{
    public class UseCaseConfiguration : EntityConfiguration<UseCase>
    {
        public override void AddConfiguration(EntityTypeBuilder<UseCase> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired();

            builder.HasMany(x => x.Users)
                   .WithOne(x => x.UseCase)
                   .HasForeignKey(x => x.UseCaseId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
