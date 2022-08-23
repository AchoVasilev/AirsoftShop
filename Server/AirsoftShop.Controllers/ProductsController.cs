namespace AirsoftShop.Controllers;

using CloudinaryDotNet;
using Common.Services;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Product;

public class ProductsController : BaseController
{
    private readonly IProductService productService;
    private readonly ICurrentUserService currentUserService;
    private readonly Cloudinary cloudinary;

    public ProductsController(IProductService productService, ICurrentUserService currentUserService, Cloudinary cloudinary)
    {
        this.productService = productService;
        this.currentUserService = currentUserService;
        this.cloudinary = cloudinary;
    }

    [HttpGet]
    [Route("newest")]
    public async Task<ActionResult> GetNewest()
        => this.Ok(await this.productService.GetNewestEightGuns());
    
    [HttpPost]
    public async Task<IActionResult> CreateGun([FromForm] GunInputModel model)
    {
        var userId = this.currentUserService.GetUserId();
        
        if (user.DealerId == null)
        {
            return BadRequest(new { ErrorMessage = MessageConstants.InvalidUserMsg });
        }

        var imageResult = await this.fileService.UploadImage(cloudinary, model.Image, NameConstants.CloudinaryFolderName);
        string? imageId;
        if (imageResult != null)
        {
            imageId = await this.fileService.AddImageToDatabase(imageResult);
        }
        else
        {
            return BadRequest(new { ErrorMessage = MessageConstants.UnsuccessfulActionMsg });
        }

        var gunId = await this.productService.CreateGunAsync(model, user.DealerId, imageId);

        return Ok(new { gunId, model.Name });
    }
}