namespace AirsoftShop.Controllers.Models.Users;

public class LoginResponseModel
{
    public string? Token { get; set; }
    
    public bool IsClient { get; set; }
}