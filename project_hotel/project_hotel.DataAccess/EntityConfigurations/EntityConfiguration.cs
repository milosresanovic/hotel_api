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
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.Property(x => x.DeletedBy).HasMaxLength(50);

            AddConfiguration(builder);
        }

        public abstract void AddConfiguration(EntityTypeBuilder<T> builder);
    }
}
