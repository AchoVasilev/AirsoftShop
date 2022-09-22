namespace AirsoftShop.Services.Common;

public interface IProduct
{
    string? Id { get; set; }

    string? Name { get; init; }
    
    public int SubCategoryId { get; set; }
}