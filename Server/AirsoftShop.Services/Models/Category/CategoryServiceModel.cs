namespace AirsoftShop.Services.Models.Category;

public class CategoryServiceModel : BasicCategoryServiceModel
{
    public IEnumerable<SubcategoryServiceModel> SubCategories { get; set; }
}