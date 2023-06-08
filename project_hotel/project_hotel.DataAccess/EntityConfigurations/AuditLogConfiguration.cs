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
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UseCase).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Time).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.IsAuthorized).IsRequired();

            builder.HasIndex(x => x.Username);
            builder.HasIndex(x => x.UseCase);
        }
    }
}
