namespace AirsoftShop.Controllers.Models.Fields;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using static Common.Constants.Messages;
using static Data.Constants.Data.Constants;

public class CreateFieldModel
{
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? StreetName { get; set; }
    
    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DescriptionMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Description { get; set; }

    public int CityId { get; set; }
    
    public int ZipCode { get; set; }
    
    [Required]
    public IEnumerable<IFormFile>? Images { get; set; }
}