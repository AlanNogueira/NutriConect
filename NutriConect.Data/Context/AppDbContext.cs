using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;

namespace NutriConect.Data.Context;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<NutritionistCredential> NutritionistCredentials => Set<NutritionistCredential>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
    public DbSet<RecipeStep> RecipeSteps => Set<RecipeStep>();
    public DbSet<Tip> Tips => Set<Tip>();
    public DbSet<TipComment> TipComments => Set<TipComment>();
    public DbSet<NutritionistReview> NutritionistReviews => Set<NutritionistReview>();
    public DbSet<RecipeReview> RecipeReviews => Set<RecipeReview>();
    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .HasDiscriminator<string>("Role")
            .HasValue<Client>("Cliente")
            .HasValue<Nutritionist>("Profissional");

        builder.Entity<Nutritionist>()
            .Property(n => n.PricePerSession)
            .HasPrecision(10, 2);

        builder.Entity<NutritionistCredential>(e =>
        {
            e.ToTable("NutritionistCredentials");
            e.HasKey(c => c.Id);
            e.Property(c => c.Description).HasMaxLength(300).IsRequired();
            e.HasOne(c => c.Nutritionist)
             .WithMany(n => n.Credentials)
             .HasForeignKey(c => c.NutritionistId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Recipe>(e =>
        {
            e.ToTable("Recipes");
            e.HasKey(r => r.Id);
            e.Property(r => r.Title).HasMaxLength(150).IsRequired();
            e.Property(r => r.ShortDescription).HasMaxLength(300);
            e.Property(r => r.PhotoUrl).HasMaxLength(400);
            e.Property(r => r.Categories).HasMaxLength(200);

            e.HasOne(r => r.Author)
             .WithMany()
             .HasForeignKey(r => r.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasMany(r => r.Ingredients)
             .WithOne(i => i.Recipe)
             .HasForeignKey(i => i.RecipeId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(r => r.Steps)
             .WithOne(s => s.Recipe)
             .HasForeignKey(s => s.RecipeId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<RecipeIngredient>(e =>
        {
            e.ToTable("RecipeIngredients");
            e.HasKey(i => i.Id);
            e.Property(i => i.Text).HasMaxLength(300).IsRequired();
        });

        builder.Entity<RecipeStep>(e =>
        {
            e.ToTable("RecipeSteps");
            e.HasKey(s => s.Id);
            e.Property(s => s.Text).HasMaxLength(1000).IsRequired();
        });

        builder.Entity<Tip>(e =>
        {
            e.ToTable("Tips");
            e.HasKey(t => t.Id);
            e.Property(t => t.Body).HasMaxLength(1000).IsRequired();

            e.HasOne(t => t.Author)
             .WithMany()
             .HasForeignKey(t => t.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasMany(t => t.Comments)
             .WithOne(c => c.Tip)
             .HasForeignKey(c => c.TipId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<TipComment>(e =>
        {
            e.ToTable("TipComments");
            e.HasKey(c => c.Id);
            e.Property(c => c.Body).HasMaxLength(800).IsRequired();

            e.HasOne(c => c.Author)
             .WithMany()
             .HasForeignKey(c => c.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<NutritionistReview>(e =>
        {
            e.ToTable("NutritionistReviews");
            e.HasKey(r => r.Id);
            e.Property(r => r.Comment).HasMaxLength(1000);

            e.HasOne(r => r.Nutritionist)
             .WithMany()
             .HasForeignKey(r => r.NutritionistId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(r => r.Author)
             .WithMany()
             .HasForeignKey(r => r.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(r => new { r.NutritionistId, r.AuthorId }).IsUnique();
        });

        builder.Entity<Conversation>(e =>
        {
            e.ToTable("Conversations");
            e.HasKey(c => c.Id);

            e.HasOne(c => c.Client)
             .WithMany()
             .HasForeignKey(c => c.ClientId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(c => c.Nutritionist)
             .WithMany()
             .HasForeignKey(c => c.NutritionistId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasMany(c => c.Messages)
             .WithOne(m => m.Conversation)
             .HasForeignKey(m => m.ConversationId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(c => new { c.ClientId, c.NutritionistId }).IsUnique();
        });

        builder.Entity<ChatMessage>(e =>
        {
            e.ToTable("ChatMessages");
            e.HasKey(m => m.Id);
            e.Property(m => m.Content).HasMaxLength(2000).IsRequired();

            e.HasOne(m => m.Sender)
             .WithMany()
             .HasForeignKey(m => m.SenderId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(m => new { m.ConversationId, m.SentAt });
        });

        builder.Entity<RecipeReview>(e =>
        {
            e.ToTable("RecipeReviews");
            e.HasKey(r => r.Id);
            e.Property(r => r.Comment).HasMaxLength(1000);

            e.HasOne(r => r.Recipe)
             .WithMany()
             .HasForeignKey(r => r.RecipeId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(r => r.Author)
             .WithMany()
             .HasForeignKey(r => r.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(r => new { r.RecipeId, r.AuthorId }).IsUnique();
        });
    }
}
