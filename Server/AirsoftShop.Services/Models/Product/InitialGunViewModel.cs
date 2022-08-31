namespace AirsoftShop.Services.Models.Product;

using Base;

public class InitialGunViewModel : BaseGunServiceModel
{
    public string DealerName { get; set; }
    
    public string DealerSiteUrl { get; set; }
    
    public double Power { get; set; }
    
    public string ImageUrl { get; set; }
}