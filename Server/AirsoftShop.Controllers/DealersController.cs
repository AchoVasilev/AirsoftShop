namespace AirsoftShop.Controllers;

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
    private readonly ICurrentUserService currentUserService;

    public DealersController(
        UserManager<ApplicationUser> userManager,
        IDealerService dealerService,
        IFileService fileService,
        ICurrentUserService currentUserService)
    {
        this.userManager = userManager;
        this.dealerService = dealerService;
        this.fileService = fileService;
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

        var imageResult = await this.fileService.UploadImage(model.Image, CloudinaryFolderName);

        if (imageResult.Failed)
        {
            return this.BadRequest(new { imageResult.ErrorMessage });
        }

        var imageId = await this.fileService.AddImageToDatabase(imageResult.Model!);

        var serviceModel = new CreateDealerServiceModel()
        {
            CityName = model.CityName,
            Image = model.Image,
            Email = model.Email,
            Name = model.Name,
            Password = model.Password,
            Phone = model.PhoneNumber,
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

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> Edit(EditDealerInputModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);
        if (user?.DealerId is null)
        {
            return this.BadRequest(new { ErrorMessage = UserNotDealerMsg });
        }

        var serviceModel = new EditDealerServiceModel()
        {
            Email = model.Email,
            CityName = model.CityName,
            DealerNumber = model.DealerNumber,
            Name = model.Name,
            Phone = model.PhoneNumber,
            SiteUrl = model.SiteUrl,
            StreetName = model.StreetName,
        };

        var result = await this.dealerService.Edit(user.DealerId, serviceModel);
        if (result.Failed)
        {
            return this.BadRequest(result.ErrorMessage);
        }
        
        return this.Ok(result.Model);
    }

    [HttpGet]
    [Route("getDealerId")]
    public async Task<ActionResult<string>> GetDealerId()
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);
        
        return user?.DealerId is null ? 
            string.Empty : 
            new JsonResult(user.DealerId);
    }
}