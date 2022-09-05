namespace AirsoftShop.Services.Models.Address;

using City;

public class AddressServiceModel
{
    public string? StreetName { get; set; }
    
    public CityServiceModel? City { get; set; }
}