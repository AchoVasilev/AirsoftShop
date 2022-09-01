namespace AirsoftShop.Services.Services.Category;

using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Category;

public class CategoryService : ICategoryService
{
    private const string GunCategoryName = "Еърсофт оръжия";
    
    private readonly ApplicationDbContext data;

    public CategoryService(ApplicationDbContext data) 
        => this.data = data;

    public async Task<IEnumerable<CategoryServiceModel>> GetAllWithSubcategories()
        => await this.data.Categories
            .Select(x => new CategoryServiceModel()
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.Image.Url,
                SubCategories = x.SubCategories.Select(y => new SubcategoryServiceModel()
                {
                    Id = y.Id,
                    Name = y.Name
                })
            })
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<BasicCategoryServiceModel>> GetFourNewestCategories()
        => await this.data.Categories
            .Select(x => new BasicCategoryServiceModel()
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.Image.Url
            })
            .Take(4)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<SubcategoryServiceModel>> GetGunSubcategories()
        => await this.data.SubCategories
            .Where(x => x.Category.Name == GunCategoryName)
            .Select(x => new SubcategoryServiceModel()
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
}