namespace AirsoftShop.Services.Services.Category;

using Models;
using Models.Categories;

public interface ICategoryService
{
    Task<IEnumerable<CategoryServiceModel>> GetAllWithSubcategories();

    Task<IEnumerable<BasicCategoryServiceModel>> GetFourNewestCategories();
}