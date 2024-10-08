﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using System.Reflection.Emit;

namespace NutriConect.Data.Context
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<ProfessionalEvaluation> ProfessionalEvaluations { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<RecipeEvaluation> RecipeEvaluations { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<TipEvaluation> TipEvaluations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ContextBase).Assembly);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
                base.OnConfiguring(optionsBuilder);
            }
        }

        public string GetConnectionString()
        {
            return "Server=localhost,1433;Database=NutriConect;User Id=root;Password=qwe123*;Integrated Security=SSPI;TrustServerCertificate=True";
        }
    }
}
