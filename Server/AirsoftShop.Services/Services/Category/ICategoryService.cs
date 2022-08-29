namespace AirsoftShop.Services.Services.Category;

using Models.Category;

public interface ICategoryService
{
    Task<IEnumerable<CategoryServiceModel>> GetAllWithSubcategories();

    Task<IEnumerable<BasicCategoryServiceModel>> GetFourNewestCategories();
}