namespace AirsoftShop.Services.Models.Client.Base;

public class ExtendedBaseClientServiceModel : BaseClientServiceModel
{
    public string StreetName { get; init; }
    
    public string CityName { get; init; }

    public string Email { get; init; }
}