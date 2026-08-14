namespace NutriConect.Business.Entities;

public class RecipeStep
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
    public int Order { get; set; }
}
