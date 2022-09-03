namespace AirsoftShop.Services.Models.WishList;

public class WishListServiceModel
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Manufacturer { get; set; }
    
    public string DateAdded { get; set; }
    
    public decimal Price { get; set; }
    
    public int Capacity { get; set; }
}