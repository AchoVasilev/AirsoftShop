namespace AirsoftShop.Services.Services.Category;

using Common.Services.Common;
using Models.Category;

public interface ICategoryService : ITransientService
{
    Task<IEnumerable<CategoryServiceModel>> GetAllWithSubcategories();

    Task<IEnumerable<BasicCategoryServiceModel>> GetFourNewestCategories();
}