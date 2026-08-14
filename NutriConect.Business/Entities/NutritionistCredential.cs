namespace NutriConect.Business.Entities;

public class NutritionistCredential
{
    public int Id { get; set; }
    public int NutritionistId { get; set; }
    public Nutritionist Nutritionist { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
}
