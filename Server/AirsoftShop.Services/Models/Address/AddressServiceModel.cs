namespace AirsoftShop.Services.Models.Address;

using Cities;

public class AddressServiceModel
{
    public string StreetName { get; set; }
    
    public CityServiceModel City { get; set; }
}