namespace AirsoftShop.Services.Models.Categories;

public class CategoryServiceModel : BasicCategoryServiceModel
{
    public IEnumerable<SubcategoryServiceModel> SubCategories { get; set; }
}