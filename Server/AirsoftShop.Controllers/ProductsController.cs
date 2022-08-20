namespace AirsoftShop.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Services.Product;

public class ProductsController : BaseController
{
    private readonly IProductService productService;

    public ProductsController(IProductService productService) 
        => this.productService = productService;

    [HttpGet]
    [Route("newest")]
    public async Task<ActionResult> GetNewest()
        => this.Ok(await this.productService.GetNewestEightGuns());
}