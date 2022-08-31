namespace AirsoftShop.Controllers.Models.Dealers;
using System.ComponentModel.DataAnnotations;
using static Data.Constants.Data.Constants;
using static Common.Constants.Messages;

public class BaseDealerModel
{
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string Name { get; set; }

    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string DealerNumber { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    public string SiteUrl { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = InvalidEmailErrorMsg)]
    public string Email { get; set; }

    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string StreetName { get; set; }

    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string CityName { get; set; }
}