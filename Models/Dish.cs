#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
namespace Chefs_N_Dishes.Models;

public class Dish
{
    [Key]
    public int DishId { get; set; }
    [Required]
    [Display(Name = "Name of Dish")]
    public string Name { get; set; }
    [Required]
    [Range(1, 5)]
    public int Tastiness { get; set; }
    [Required]
    [UnderZero]
    [Display(Name = "# of Calories")]
    public int Calories { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int ChefId { get; set; }
    public Chef? Cook { get; set; }
}

public class UnderZeroAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if ((int)value < 0)
        {
            return new ValidationResult("Number must be positive!");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}