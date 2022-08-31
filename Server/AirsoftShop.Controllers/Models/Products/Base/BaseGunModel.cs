namespace AirsoftShop.Controllers.Models.Products.Base;

using System.ComponentModel.DataAnnotations;
using static Data.Constants.Data.Constants;
using static Common.Constants.Messages;

public abstract class BaseGunModel
{
    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string Name { get; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string Manufacturer { get; }
    
    [Range(RangeMinLength, RangeMaxLength, ErrorMessage = LengthErrorMsg)]
    public double Power { get; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string Color { get; }

    [Range(RangeMinLength, maximum: NumbersMaxLength, ErrorMessage = LengthErrorMsg)]
    public double Weight { get; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string Magazine { get; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [Range(RangeMinLength, RangeMaxLength, ErrorMessage = LengthErrorMsg)]
    public int Capacity { get; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [Range(RangeMinLength, RangeMaxLength, ErrorMessage = LengthErrorMsg)]
    public int Speed { get; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string Firing { get; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [Range(RangeMinLength, NumbersMaxLength, ErrorMessage = LengthErrorMsg)]
    public int Length { get; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [Range(RangeMinLength, NumbersMaxLength, ErrorMessage = LengthErrorMsg)]
    public int Barrel { get; }

    public string Propulsion { get; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string Material { get; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string Blowback { get; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string Hopup { get; }

    public decimal Price { get; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string SubCategoryName { get; }
}