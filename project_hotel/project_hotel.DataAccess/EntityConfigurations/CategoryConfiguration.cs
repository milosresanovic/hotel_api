using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.DataAccess.EntityConfigurations
{
    public class CategoryConfiguration : EntityConfiguration<Category>
    {
        public override void AddConfiguration(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ParentId).IsRequired(false);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.ChildCategories)
                   .WithOne(x => x.Parent)
                   .HasForeignKey(x => x.ParentId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
