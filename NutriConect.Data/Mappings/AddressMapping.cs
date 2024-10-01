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
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.City).IsRequired().HasColumnType("varchar(256)");

            builder.Property(x => x.ZipCode).IsRequired().HasColumnType("varchar(256)");

            builder.Property(x => x.State).IsRequired().HasColumnType("varchar(256)");

            builder.Property(x => x.Street).IsRequired().HasColumnType("varchar(256)");

            builder.Property(x => x.Neighborhood).IsRequired().HasColumnType("varchar(256)");

            builder.Property(x => x.Number).IsRequired().HasColumnType("varchar(256)");

            builder.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.Now);

            builder.ToTable("Addresses");
        }
    }
}
