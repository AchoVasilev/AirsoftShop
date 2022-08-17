namespace AirsoftShop.Controllers.Models.Users;

using System.ComponentModel.DataAnnotations;

public class LoginUserRequestModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}