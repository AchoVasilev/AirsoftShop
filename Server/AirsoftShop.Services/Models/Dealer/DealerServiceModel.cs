namespace AirsoftShop.Services.Models.Dealer;

using Address;

public class DealerServiceModel
{
    public string? Name { get; set; }
    
    public string? DealerNumber { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public string? SiteUrl { get; set; }
    
    public AddressServiceModel? Address { get; set; }
}