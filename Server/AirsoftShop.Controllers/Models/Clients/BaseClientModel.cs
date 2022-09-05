namespace AirsoftShop.Controllers.Models.Clients;
using System.ComponentModel.DataAnnotations;
using static Data.Constants.Data.Constants;
using static Common.Constants.Messages;

public abstract class BaseClientModel
{
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? FirstName { get; init; }
    
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? LastName { get; init; }
    
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? StreetName { get; init; }
    
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? CityName { get; init; }
    
    [Required]
    [Phone]
    public string? PhoneNumber { get; init; }
    
    [Required]
    [EmailAddress(ErrorMessage = InvalidEmailErrorMsg)]
    public string? Email { get; init; }
}