using NutriConect.Business.Enums;

namespace NutriConect.Business.ViewModels;

public class TipFeedViewModel
{
    public List<TipCardViewModel> Tips { get; set; } = [];
    public TipCategoryEnum? Category { get; set; }
    public bool OnlyProfessionals { get; set; }
    public int Total => Tips.Count;
}
