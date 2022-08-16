namespace AirsoftShop.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Services.Category;

public class CategoriesController : BaseController
{
    private readonly ICategoryService categoryService;

    public CategoriesController(ICategoryService categoryService) 
        => this.categoryService = categoryService;

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult> GetAll() 
        => this.Ok(await this.categoryService.GetAllWithSubcategories());
}