namespace AirsoftShop.Services.Models.Product.Clothings;

using File;

public class CreateProductServiceModel
{
    public CreateProductServiceModel()
    {
        this.Images = new List<IFileServiceModel>();
    }

    public string? Name { get; set; }

    public string? Manufacturer { get; set; }

    public decimal Price { get; set; }

    public int SubcategoryId { get; set; }

    public string? Description { get; set; }
    
    public string? Color { get; set; }
    
    public int Size { get; set; }

    public string? Material { get; set; }

    public IEnumerable<IFileServiceModel> Images { get; set; }
}