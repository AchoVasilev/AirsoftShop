namespace AirsoftShop.Controllers;

using CloudinaryDotNet;
using Common.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Products;
using Services.Models.Products;
using Services.Services.File;
using Services.Services.Product;
using static Common.Constants.Constants.WebConstants;
using static Common.Constants.Messages;

public class ProductsController : BaseController
{
    private readonly IProductService productService;
    private readonly ICurrentUserService currentUserService;
    private readonly IFileService fileService;
    private readonly Cloudinary cloudinary;
    private readonly UserManager<ApplicationUser> userManager;

    public ProductsController(
        IProductService productService,
        ICurrentUserService currentUserService,
        IFileService fileService,
        Cloudinary cloudinary,
        UserManager<ApplicationUser> userManager)
    {
        this.productService = productService;
        this.currentUserService = currentUserService;
        this.fileService = fileService;
        this.cloudinary = cloudinary;
        this.userManager = userManager;
    }

    [HttpGet]
    [Route("newest")]
    public async Task<ActionResult> GetNewest()
        => this.Ok(await this.productService.GetNewestEightGuns());

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateGun([FromForm] GunInputModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        if (user.DealerId is null)
        {
            return this.Unauthorized(new { ErrorMessage = InvalidUserMsg });
        }

        var imageIds = new List<string>();
        foreach (var image in model.Images)
        {
            var imageResult = await this.fileService.UploadImage(this.cloudinary, image, CloudinaryFolderName);
            if (imageResult.Failed)
            {
                return this.BadRequest(new { ErrorMessage = UnsuccessfulActionMsg });
            }

            var imageId = await this.fileService.AddImageToDatabase(imageResult.Model);
            imageIds.Add(imageId);
        }

        var gunModel = new CreateGunServiceModel()
        {
            Barrel = model.Barrel,
            Blowback = model.Blowback,
            Capacity = model.Capacity,
            Color = model.Color,
            Firing = model.Firing,
            Hopup = model.Hopup,
            ImageIds = imageIds,
            Length = model.Length,
            Magazine = model.Magazine,
            Manufacturer = model.Manufacturer,
            Material = model.Material,
            Name = model.Name,
            Speed = model.Speed,
            SubCategoryName = model.SubCategoryName,
            Power = model.Power,
            Price = model.Price,
            Weight = model.Weight,
            Propulsion = model.Propulsion,
        };

        var result = await this.productService.CreateGun(gunModel, user.DealerId);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.CreatedAtAction(nameof(this.CreateGun), result.Model);
    }
    
    [AllowAnonymous]
    [HttpGet]
    [Route("details")]
    public async Task<IActionResult> GetDetails([FromQuery] string gunId)
    {
        var res = await this.productService.GetDetails(gunId);
        if (res is null)
        {
            return this.NotFound();
        }
        
        return this.Ok(res);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> EditGun([FromBody] GunEditModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        if (user.DealerId is null)
        {
            return this.Unauthorized(new { ErrorMessage = InvalidUserMsg });
        }
        
        var editModel = new EditGunServiceModel()
        {
            Barrel = model.Barrel,
            Blowback = model.Blowback,
            Capacity = model.Capacity,
            Color = model.Color,
            Firing = model.Firing,
            Hopup = model.Hopup,
            Length = model.Length,
            Magazine = model.Magazine,
            Manufacturer = model.Manufacturer,
            Material = model.Material,
            Name = model.Name,
            Speed = model.Speed,
            SubCategoryName = model.SubCategoryName,
            Power = model.Power,
            Price = model.Price,
            Weight = model.Weight,
            Propulsion = model.Propulsion,
        };
        
        var result = await this.productService.Edit(user.DealerId, editModel);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.Ok(result.Model);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGun([FromBody] string gunId)
    {
        var result = await this.productService.DeleteGunAsync(gunId);
        if (result == false)
        {
            return BadRequest(new { ErrorMessage = MessageConstants.UnsuccessfulActionMsg });
        }

        return Ok(result);
    }

}