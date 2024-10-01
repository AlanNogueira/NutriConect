using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutriConect.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(256)");

            builder.Property(x => x.Phone).IsRequired().HasColumnType("varchar(256)");

            builder.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.Now);

            builder.HasOne(x => x.Address);

            builder.ToTable("Clients");
        }
    }
}
