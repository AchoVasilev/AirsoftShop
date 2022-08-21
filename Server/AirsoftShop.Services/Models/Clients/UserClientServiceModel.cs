namespace AirsoftShop.Services.Models.Clients;

public class UserClientServiceModel
{
    public string UserId { get; set; }
    
    public string Email { get; set; }
    
    public string Username { get; set; }
    
    public string ClientId { get; set; }
    
    public ClientServiceModel Client { get; set; }
    
    public string ImageUrl { get; set; }
}