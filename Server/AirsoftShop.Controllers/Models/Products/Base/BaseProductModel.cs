namespace AirsoftShop.Controllers.Models.Products.Base;

using System.ComponentModel.DataAnnotations;
using static Common.Constants.Messages;
using static Data.Constants.Data.Constants;
public class BaseProductModel
{
    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Name { get; set; }

    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Manufacturer { get; set; }

    public decimal Price { get; set; }
    
    public int SubcategoryId { get; set; }
    
    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? SubCategoryName { get; set; }
    
    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DescriptionMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Description { get; set; }
}