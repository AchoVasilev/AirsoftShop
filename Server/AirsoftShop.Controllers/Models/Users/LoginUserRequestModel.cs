namespace AirsoftShop.Controllers.Models.Users;

using System.ComponentModel.DataAnnotations;
using static Data.Constants.Data.Constants;
using static Common.Constants.Messages;

public class LoginUserRequestModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    
    [Required]
    [MinLength(PasswordMinLength, ErrorMessage = PasswordLengthErrorMsg)]
    public string? Password { get; set; }
}