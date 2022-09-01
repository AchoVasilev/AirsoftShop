namespace AirsoftShop.Controllers;

using AirsoftShop.Services.Services.Category;
using Microsoft.AspNetCore.Mvc;

public class CategoriesController : BaseController
{
    private readonly ICategoryService categoryService;

    public CategoriesController(ICategoryService categoryService) 
        => this.categoryService = categoryService;

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult> GetAll() 
        => this.Ok(await this.categoryService.GetAllWithSubcategories());

    [HttpGet]
    [Route("newest")]
    public async Task<ActionResult> GetNewest()
        => this.Ok(await this.categoryService.GetFourNewestCategories());

    [HttpGet]
    [Route("gunSubcategories")]
    public async Task<ActionResult> GetSubcategories()
        => this.Ok(await this.categoryService.GetGunSubcategories());
}