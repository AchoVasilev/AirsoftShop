namespace AirsoftShop.Controllers;

using CloudinaryDotNet;
using Common.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Dealers;
using Services.Models.Dealer;
using Services.Services.Dealer;
using Services.Services.File;
using static Common.Constants.Messages;
using static Common.Constants.Constants.WebConstants;

public class DealersController : BaseController
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IDealerService dealerService;
    private readonly IFileService fileService;
    private readonly Cloudinary cloudinary;
    private readonly ICurrentUserService currentUserService;

    public DealersController(
        UserManager<ApplicationUser> userManager,
        IDealerService dealerService,
        IFileService fileService,
        Cloudinary cloudinary, ICurrentUserService currentUserService)
    {
        this.userManager = userManager;
        this.dealerService = dealerService;
        this.fileService = fileService;
        this.cloudinary = cloudinary;
        this.currentUserService = currentUserService;
    }

    [HttpPost]
    public async Task<ActionResult> Register([FromForm] CreateDealerInputModel model)
    {
        var user = await this.userManager.FindByNameAsync(model.Username);
        if (user != null)
        {
            return this.BadRequest(new { ErrorMessage = UsernameExistsMsg });
        }

        var imageResult = await this.fileService.UploadImage(this.cloudinary, model.Image, CloudinaryFolderName);

        if (imageResult.Failed)
        {
            return this.BadRequest(new { imageResult.ErrorMessage });
        }

        var imageId = await this.fileService.AddImageToDatabase(imageResult.Model);

        var serviceModel = new CreateDealerServiceModel()
        {
            CityName = model.CityName,
            Image = model.Image,
            Email = model.Email,
            Name = model.Name,
            Password = model.Password,
            Phone = model.Phone,
            DealerNumber = model.DealerNumber,
            SiteUrl = model.SiteUrl,
            StreetName = model.StreetName,
            Username = model.Username
        };

        var result = await this.dealerService.CreateDealer(serviceModel, imageId);
        if (result.Succeeded)
        {
            return this.CreatedAtAction(nameof(this.Register), result.Model);
        }

        return this.BadRequest(new { ErrorMessage = UnsuccessfulActionMsg, result.ErrorMessages });
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> Profile()
    {
        var userId = this.currentUserService.GetUserId();

        var result = await this.dealerService.Profile(userId!);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }
        
        return this.Ok(result.Model);
    }
}