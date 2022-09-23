namespace AirsoftShop.Controllers.Products;

using Attributes;
using Common.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Products.Guns;
using Services.Models.File;
using Services.Models.Product.Guns;
using Services.Services.Common;
using Services.Services.File;
using static Common.Constants.Constants.WebConstants;
using static Common.Constants.Messages;
public class GunsController : BaseController
{
    private readonly ICurrentUserService currentUserService;
    private readonly IGunService gunService;
    private readonly IFileService fileService;
    private readonly UserManager<ApplicationUser> userManager;

    public GunsController(
        ICurrentUserService currentUserService,
        IGunService gunService,
        IFileService fileService,
        UserManager<ApplicationUser> userManager)
    {
        this.gunService = gunService;
        this.currentUserService = currentUserService;
        this.gunService = gunService;
        this.fileService = fileService;
        this.userManager = userManager;
    }

    [HttpGet]
    [Route("newest")]
    public async Task<ActionResult> GetNewest()
        => this.Ok(await this.gunService.GetNewestEightGuns());

    [HttpPost]
    [Authorize]
    [ValidateDealer]
    public async Task<IActionResult> CreateGun([FromForm] GunInputModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        var fileModels = new List<IFileServiceModel>();
        foreach (var image in model.Images)
        {
            var imageResult = await this.fileService.UploadImage(image, CloudinaryFolderName);
            if (imageResult.Failed)
            {
                return this.BadRequest(new { ErrorMessage = UnsuccessfulActionMsg });
            }

            fileModels.Add(imageResult.Model!);
        }

        var gunModel = new CreateGunServiceModel()
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
            SubCategoryId = model.SubcategoryId,
            Power = model.Power,
            Price = model.Price,
            Weight = model.Weight,
            Propulsion = model.Propulsion,
            Images = fileModels,
            Description = model.Description,
        };

        var result = await this.gunService.Create(gunModel, user.DealerId);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.CreatedAtAction(nameof(this.CreateGun), result.Model);
    }

    [HttpGet]
    [Route("{gunId}")]
    public async Task<IActionResult> GetDetails(string gunId)
    {
        var res = await this.gunService.GetDetails(gunId);
        if (res is null)
        {
            return this.NotFound();
        }

        return this.Ok(res);
    }

    [Authorize]
    [HttpPut]
    [ValidateDealer]
    public async Task<IActionResult> EditGun([FromBody] GunEditModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

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
            SubCategoryId = model.SubcategoryId,
            Power = model.Power,
            Price = model.Price,
            Weight = model.Weight,
            Propulsion = model.Propulsion,
            Description = model.Description,
        };

        var result = await this.gunService.Edit(user.DealerId, editModel);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.Ok(result.Model);
    }

    [HttpDelete]
    [Authorize]
    [ValidateDealer]
    public async Task<IActionResult> DeleteGun([FromBody] string gunId)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        var result = await this.gunService.DeleteGun(gunId, user.DealerId);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.Ok(result);
    }

    [HttpGet]
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

        var guns = await this.gunService.GetAllGuns(queryModel);
        var allGunsViewModel = new GunsViewModel
        {
            AllGuns = guns,
            ItemsPerPage = query.ItemsPerPage,
            PageNumber = query.Page
        };

        const string allStr = "all";
        const string nullStr = "null";
        var isBasicCategoryString = string.IsNullOrEmpty(query.CategoryName) ||
                                    query.CategoryName.ToLower() == allStr ||
                                    query.CategoryName == nullStr;

        if (isBasicCategoryString)
        {
            allGunsViewModel.Colors = await this.gunService.GetAllColors();
            allGunsViewModel.Manufacturers = await this.gunService.GetAllManufacturers();
            allGunsViewModel.Dealers = await this.gunService.GetAllDealers();
            allGunsViewModel.Powers = await this.gunService.GetAllPowers();
            allGunsViewModel.ItemCount = await this.gunService.GetAllGunsCount();
        }
        else
        {
            var gunColors = new HashSet<string>();
            var gunManufacturers = new HashSet<string>();
            var gunDealers = new HashSet<string>();
            var gunPowers = new HashSet<double>();

            foreach (var gun in guns)
            {
                gunColors.Add(gun.Color!);
                gunManufacturers.Add(gun.Manufacturer!);
                gunDealers.Add(gun.DealerName!);
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
        var result = await this.gunService.GetMyProducts(userId!);

        if (result.Failed)
        {
            return this.Unauthorized(new { result.ErrorMessage });
        }

        return this.Ok(result.Models);
    }
}