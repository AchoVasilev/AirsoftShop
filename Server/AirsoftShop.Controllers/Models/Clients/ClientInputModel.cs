namespace AirsoftShop.Controllers.Models.Clients;

using System.ComponentModel.DataAnnotations;
using static Data.Constants.Data.Constants;
using static Common.Constants.Messages;

public class ClientInputModel : BaseClientModel
{
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Username { get; set; }
    
    [Required]
    [StringLength(DefaultMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Password { get; set; }
}