﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutriConect.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Data.Mappings
{
    public class TipMapping : IEntityTypeConfiguration<Tip>
    {
        public void Configure(EntityTypeBuilder<Tip> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasColumnType("varchar(256)");

            builder.Property(x => x.Text).IsRequired().HasColumnType("varchar(max)");

            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");

            builder.HasOne(x => x.Client);

            builder.HasOne(x => x.Professional);

            builder.ToTable("Tips");
        }
    }
}
