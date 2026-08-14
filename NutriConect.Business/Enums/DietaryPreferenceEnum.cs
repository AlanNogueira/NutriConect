using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.Enums;

public enum DietaryPreferenceEnum
{
    [Display(Name = "Onívora")]    Onivora     = 0,
    [Display(Name = "Vegetariana")]Vegetariana = 1,
    [Display(Name = "Vegana")]     Vegana      = 2,
    [Display(Name = "Low carb")]   LowCarb     = 3
}
