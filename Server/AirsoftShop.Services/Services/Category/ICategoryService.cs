namespace AirsoftShop.Services.Services.Category;

using Common;
using Models.Category;

public interface ICategoryService : ITransientService
{
    Task<IEnumerable<CategoryServiceModel>> GetAllWithSubcategories();

    Task<IEnumerable<BasicCategoryServiceModel>> GetFourNewestCategories();

    Task<IEnumerable<SubcategoryServiceModel>> GetGunSubcategories();

    Task<IEnumerable<SubcategoryServiceModel>> GetClothingSubcategories();
}