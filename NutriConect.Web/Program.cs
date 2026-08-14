using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using NutriConect.Business.Identity;
using NutriConect.Business.Interfaces;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.Services;
using NutriConect.Data;
using NutriConect.Data.Context;
using NutriConect.Data.Repositories;
using NutriConect.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders()
.AddErrorDescriber<PortugueseIdentityErrorDescriber>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<INutritionistRepository, NutritionistRepository>();
builder.Services.AddScoped<INutritionistReviewRepository, NutritionistReviewRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeReviewRepository, RecipeReviewRepository>();
builder.Services.AddScoped<ITipRepository, TipRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<INutritionistService, NutritionistService>();
builder.Services.AddScoped<INutritionistReviewService, NutritionistReviewService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IRecipeReviewService, RecipeReviewService>();
builder.Services.AddScoped<ITipService, TipService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

var ptBR = new CultureInfo("pt-BR");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ptBR),
    SupportedCultures = [ptBR],
    SupportedUICultures = [ptBR]
});

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapHub<ChatHub>("/hubs/chat");

await EnsureDatabaseMigratedAsync(app);
await SeedRolesAsync(app);
await SeedTestUserAsync(app);

app.Run();

static async Task EnsureDatabaseMigratedAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

static async Task SeedRolesAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    foreach (var role in new[] { "Cliente", "Profissional" })
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole<int>(role));
    }
}

static async Task SeedTestUserAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    const string testEmail = "teste@nutriconect.com";
    if (await userManager.FindByEmailAsync(testEmail) is null)
    {
        var client = new Client
        {
            UserName = testEmail,
            Email = testEmail,
            FullName = "Usuário Teste",
            AcceptedTerms = true,
        };
        await userManager.CreateAsync(client, "Senha123!");
        await userManager.AddToRoleAsync(client, "Cliente");
    }
}
