namespace AirsoftShop.Controllers.Models.Users;

using System.ComponentModel.DataAnnotations;
using static Data.Constants.Data.Constants;

public class LoginUserRequestModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(PasswordMinLength)]
    public string Password { get; set; }
}