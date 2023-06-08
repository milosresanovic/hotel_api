using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace project_hotel.DataAccess.EntityConfigurations
{
    public class CommentConfiguration : EntityConfiguration<Comment>
    {
        public override void AddConfiguration(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Text).HasMaxLength(500).IsRequired();

            builder.Property(x => x.StarNumber).IsRequired();

            
        }
    }
}
