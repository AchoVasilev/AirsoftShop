namespace AirsoftShop.Services.Models.Field;

using Address;

public class FieldDetailsServiceModel
{
    public int Id { get; set; }
    
    public string? Description { get; set; }
    
    public string? DealerName { get; set; }
    
    public string? DealerPhone { get; set; }
    
    public AddressServiceModel? Address { get; set; }
    
    public IEnumerable<string>? ImageUrls { get; set; }
}