namespace AirsoftShop.Services.Models.Client;

using Address;

public class ClientServiceModel
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public AddressServiceModel Address { get; set; }
}