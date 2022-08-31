namespace AirsoftShop.Services.Models.Client;

using Address;
using Base;

public class ClientServiceModel : BaseClientServiceModel
{
    public AddressServiceModel Address { get; set; }
}