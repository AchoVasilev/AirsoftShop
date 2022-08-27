namespace AirsoftShop.Services.Models.Dealer;

public class UserDealerServiceModel
{
    public string UserId { get; set; }
    
    public string Email { get; set; }
    
    public string UserName { get; set; }
    
    public string DealerId { get; set; }
    
    public DealerServiceModel Dealer { get; set; }
    
    public string ImageUrl { get; set; }
}