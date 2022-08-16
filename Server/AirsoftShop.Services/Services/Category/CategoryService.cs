namespace AirsoftShop.Services.Services.Category;

using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext data;

    public CategoryService(ApplicationDbContext data) 
        => this.data = data;

    public async Task<IEnumerable<CategoryServiceModel>> GetAllWithSubcategories()
        => await this.data.Categories
            .Select(x => new CategoryServiceModel()
            {
                Id = x.Id,
                Name = x.Name,
                Image = new ImageServiceModel()
                {
                    Id = x.Image.Id,
                    Url = x.Image.Url
                },
                SubCategories = x.SubCategories.Select(y => new SubcategoryServiceModel()
                {
                    Id = y.Id,
                    Name = y.Name
                })
            })
            .AsNoTracking()
            .ToListAsync();
}