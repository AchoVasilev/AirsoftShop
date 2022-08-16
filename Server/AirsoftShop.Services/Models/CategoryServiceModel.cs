namespace AirsoftShop.Services.Models;

public class CategoryServiceModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public ImageServiceModel Image { get; set; }
    
    public IEnumerable<SubcategoryServiceModel> SubCategories { get; set; }
}