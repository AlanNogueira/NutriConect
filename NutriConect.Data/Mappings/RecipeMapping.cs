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
    public class RecipeMapping : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(256)");

            builder.Property(x => x.Text).IsRequired().HasColumnType("varchar(MAX)");

            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");

            builder.HasOne(x => x.Client);

            builder.HasOne(x => x.Professional);

            builder.HasMany(x => x.Evaluations);

            builder.ToTable("Recipes");
        }
    }
}
