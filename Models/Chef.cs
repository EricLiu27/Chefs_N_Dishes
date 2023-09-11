#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Chefs_N_Dishes.Models;

public class Chef
{
    [Key]
    public int ChefId { get; set; }
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [PastDate]
    [Display(Name = "Date of Birth")]
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Dish> Dishes { get; set; } = new();


    public int Age()
    {
        TimeSpan age = DateTime.Now - DateOfBirth;
        return age.Days / 365;
    }
}

public class PastDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if ((DateTime)value > DateTime.Now)
        {
            return new ValidationResult("Date of Birth must be in the past!");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}