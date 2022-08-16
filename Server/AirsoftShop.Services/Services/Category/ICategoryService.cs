namespace AirsoftShop.Services.Services.Category;

using Models;

public interface ICategoryService
{
    Task<IEnumerable<CategoryServiceModel>> GetAllWithSubcategories();
}