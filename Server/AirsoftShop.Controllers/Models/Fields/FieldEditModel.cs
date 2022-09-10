namespace AirsoftShop.Controllers.Models.Fields;

using System.ComponentModel.DataAnnotations;
using static Common.Constants.Messages;
using static Data.Constants.Data.Constants;
public class FieldEditModel
{
    public int Id { get; set; }
    
    public int CityId { get; set; }
    
    [Required]
    public string? DealerId { get; set; }
    
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? StreetName { get; set; }
    
    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DescriptionMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Description { get; set; }
    
    public int ZipCode { get; set; }
}