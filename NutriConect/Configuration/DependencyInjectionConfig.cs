using NutriConect.Business.Interfaces.Repository;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.Services;
using NutriConect.Data.Context;
using NutriConect.Data.Repository;

namespace NutriConect.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ContextBase>();

            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();

            services.AddScoped<IProfessionalService, ProfessionalService>();
            services.AddScoped<IProfessionalRepository, ProfessionalRepository>();

            services.AddScoped<IRecipeEvaluationRepository, RecipeEvaluationRepository>();

            services.AddScoped<ITipService, TipService>();
            services.AddScoped<ITipRepository, TipRepository>();

            return services;
        }
    }
}
