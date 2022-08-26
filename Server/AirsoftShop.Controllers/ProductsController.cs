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
            return this.Unauthorized(new { ErrorMessage = NotAuthorizedMsg });
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
            return this.Unauthorized(new { ErrorMessage = NotAuthorizedMsg });
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
    [Authorize]
    public async Task<IActionResult> DeleteGun([FromBody] string gunId)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        if (user.DealerId is null)
        {
            return this.Unauthorized(new { ErrorMessage = NotAuthorizedMsg });
        }
        
        var result = await this.productService.DeleteGun(gunId, user.DealerId);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.Ok(result);
    }
    
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll([FromQuery] AllGunsQueryModel query)
    {
        var queryModel = new GunsQueryServiceModel()
        {
            CategoryName = query.CategoryName,
            Colors = query.Colors,
            Dealers = query.Dealers,
            ItemsPerPage = query.ItemsPerPage,
            Manufacturers = query.Manufacturers,
            OrderBy = query.OrderBy,
            Page = query.Page,
            Powers = query.Powers
        };
        
        var guns = await this.productService.GetAllGuns(queryModel);
        var allGunsViewModel = new GunsViewModel
        {
            AllGuns = guns,
            ItemsPerPage = query.ItemsPerPage,
            PageNumber = query.Page
        };

        if (string.IsNullOrEmpty(query.CategoryName) || query.CategoryName.ToLower() == "all" || query.CategoryName == "null")
        {
            allGunsViewModel.Colors = await this.productService.GetAllColors();
            allGunsViewModel.Manufacturers = await this.productService.GetAllManufacturers();
            allGunsViewModel.Dealers = await this.productService.GetAllDealers();
            allGunsViewModel.Powers = await this.productService.GetAllPowers();
            allGunsViewModel.ItemCount = await this.productService.GetAllGunsCount();
        }
        else
        {
            var gunColors = new HashSet<string>();
            var gunManufacturers = new HashSet<string>();
            var gunDealers = new HashSet<string>();
            var gunPowers = new HashSet<double>();

            foreach (var gun in guns)
            {
                gunColors.Add(gun.Color);
                gunManufacturers.Add(gun.Manufacturer);
                gunDealers.Add(gun.DealerName);
                gunPowers.Add(gun.Power);
            }

            allGunsViewModel.Colors = gunColors;
            allGunsViewModel.Manufacturers = gunManufacturers;
            allGunsViewModel.Dealers = gunDealers;
            allGunsViewModel.Powers = gunPowers;
            allGunsViewModel.ItemCount = guns.Count;
        }
            

        return this.Ok(allGunsViewModel);
    }
    
    [HttpGet]
    [Route("mine")]
    [Authorize]
    public async Task<IActionResult> MyProducts()
    {
        var userId = this.currentUserService.GetUserId();
        var result = await this.productService.GetMyProducts(userId!);

        if (result.Failed)
        {
            return this.Unauthorized(new { result.ErrorMessage });
        }
        
        return this.Ok(result.Models);
    }
}