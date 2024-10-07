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
    class ProfessionalEvaluationMapping : IEntityTypeConfiguration<ProfessionalEvaluation>
    {
        public void Configure(EntityTypeBuilder<ProfessionalEvaluation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasColumnType("varchar(256)");

            builder.Property(x => x.Text).IsRequired().HasColumnType("varchar(max)");

            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");

            builder.Property(x => x.Value).HasColumnType("int");

            builder.Property(x => x.ClientId).HasColumnType("int");

            builder.Property(x => x.ProfessionalId).HasColumnType("int");

            builder.ToTable("ProfessionalEvaluations");
        }
    }
}
