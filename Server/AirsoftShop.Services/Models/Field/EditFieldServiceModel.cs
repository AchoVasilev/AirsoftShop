namespace AirsoftShop.Services.Models.Field;

public class EditFieldServiceModel
{
    public int Id { get; init; }
    
    public int CityId { get; init; }
    
    public string? DealerId { get; init; }
    
    public string? StreetName { get; init; }
    
    public string? Description { get; init; }
    
    public int ZipCode { get; set; }
}