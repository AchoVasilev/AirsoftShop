namespace AirsoftShop.Services.Models.Field;

using Address;
using File;

public class CreateFieldServiceModel
{
    public string? StreetName { get; set; }

    public int CityId { get; set; }
    
    public string? CityName { get; set; }
    
    public int ZipCode { get; set; }
    
    public IEnumerable<IFileServiceModel>? Images { get; set; }
}