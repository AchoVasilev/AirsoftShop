namespace AirsoftShop.Controllers.Models.Products.Base;

using System.ComponentModel.DataAnnotations;
using static Data.Constants.Data.Constants;
using static Common.Constants.Messages;

public abstract class BaseGunModel
{
    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Name { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Manufacturer { get; set; }

    [Range(RangeMinLength, RangeMaxLength, ErrorMessage = LengthErrorMsg)]
    public double Power { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Color { get; set; }

    [Range(RangeMinLength, maximum: NumbersMaxLength, ErrorMessage = LengthErrorMsg)]
    public double Weight { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Magazine { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [Range(RangeMinLength, RangeMaxLength, ErrorMessage = LengthErrorMsg)]
    public int Capacity { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [Range(RangeMinLength, RangeMaxLength, ErrorMessage = LengthErrorMsg)]
    public int Speed { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Firing { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [Range(RangeMinLength, NumbersMaxLength, ErrorMessage = LengthErrorMsg)]
    public int Length { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [Range(RangeMinLength, NumbersMaxLength, ErrorMessage = LengthErrorMsg)]
    public int Barrel { get; set; }

    [Required]
    public string? Propulsion { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Material { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Blowback { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Hopup { get; set; }

    public decimal Price { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? SubCategoryName { get; set; }
}