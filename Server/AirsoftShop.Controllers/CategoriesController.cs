namespace AirsoftShop.Controllers;

using AirsoftShop.Services.Services.Category;
using Microsoft.AspNetCore.Mvc;
using static Common.Constants.Constants.ControllerRoutes;

public class CategoriesController : BaseController
{
    private readonly ICategoryService categoryService;

    public CategoriesController(ICategoryService categoryService) 
        => this.categoryService = categoryService;

    [HttpGet]
    public async Task<ActionResult> GetAll() 
        => this.Ok(await this.categoryService.GetAllWithSubcategories());

    [HttpGet]
    [Route(Newest)]
    public async Task<ActionResult> GetNewest()
        => this.Ok(await this.categoryService.GetFourNewestCategories());

    [HttpGet]
    [Route(GunSubcategories)]
    public async Task<ActionResult> GetGunSubcategories()
        => this.Ok(await this.categoryService.GetGunSubcategories());
    
    [HttpGet]
    [Route(ClothingSubcategories)]
    public async Task<ActionResult> GetClothingSubcategories()
        => this.Ok(await this.categoryService.GetClothingSubcategories());
}