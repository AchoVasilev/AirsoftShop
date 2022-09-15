namespace AirsoftShop.Controllers.Products;

using Common.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Products.Clothings;
using Services.Models.File;
using Services.Models.Product.Clothings;
using Services.Services.File;
using Services.Services.Product.Clothing;
using static Common.Constants.Messages;
using static Common.Constants.Constants.WebConstants;
public class ClothingsController : BaseController
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ICurrentUserService currentUserService;
    private readonly IFileService fileService;
    private readonly IClothingService clothingService;

    public ClothingsController(
        UserManager<ApplicationUser> userManager,
        ICurrentUserService currentUserService,
        IFileService fileService,
        IClothingService clothingService)
    {
        this.userManager = userManager;
        this.currentUserService = currentUserService;
        this.fileService = fileService;
        this.clothingService = clothingService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateGun([FromForm]CreateClothingInputModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        if (user.DealerId is null)
        {
            return this.Unauthorized(new { ErrorMessage = NotAuthorizedMsg });
        }

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

        var serviceModel = new CreateProductServiceModel()
        {
            Manufacturer = model.Manufacturer,
            Description = model.Description,
            Material = model.Material,
            Size = model.Size,
            Price = model.Price,
            SubcategoryId = model.SubcategoryId,
            SubCategoryName = model.SubCategoryName,
            Name = model.Name,
            Images = fileModels,
        };

        var result = await this.clothingService.Create(serviceModel, user.DealerId);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.CreatedAtAction(nameof(this.CreateGun), result.Model);
    }
}