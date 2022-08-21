namespace AirsoftShop.Controllers.Models.Clients;

using System.ComponentModel.DataAnnotations;
using static Data.Constants.Data.Constants;
using static Common.Constants.Messages;

public class EditClientFormModel
{
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string StreetName { get; set; }
    
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string CityName { get; set; }
    
    [Required]
    [Phone]
    public string Phone { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}