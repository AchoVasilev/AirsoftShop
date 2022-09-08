namespace AirsoftShop.Controllers;

using Common.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Fields;
using Services.Models.Field;
using Services.Models.File;
using Services.Services.Field;
using Services.Services.File;
using static Common.Constants.Constants.WebConstants;
using static Common.Constants.Messages;
public class FieldsController : BaseController
{
    private readonly IFieldService fieldService;
    private readonly ICurrentUserService currentUserService;
    private readonly IFileService fileService;
    private readonly UserManager<ApplicationUser> userManager;

    public FieldsController(
        IFieldService fieldService, 
        ICurrentUserService currentUserService,
        IFileService fileService,
        UserManager<ApplicationUser> userManager)
    {
        this.fieldService = fieldService;
        this.currentUserService = currentUserService;
        this.fileService = fileService;
        this.userManager = userManager;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Create([FromBody]CreateFieldModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);
        if (user?.DealerId is null)
        {
            return this.BadRequest(new { ErrorMessage = UserNotDealerMsg });
        }
        
        var fileModels = new List<IFileServiceModel>();
        foreach (var image in model.Images!)
        {
            var imageResult = await this.fileService.UploadImage(image, CloudinaryFolderName);
            if (imageResult.Failed)
            {
                return this.BadRequest(new { imageResult.ErrorMessage });
            }

            fileModels.Add(imageResult.Model!);
        }

        var serviceModel = new CreateFieldServiceModel()
        {
            CityId = model.CityId,
            StreetName = model.StreetName,
            ZipCode = model.ZipCode,
            Images = fileModels
        };

        var result = await this.fieldService.Create(user.DealerId, serviceModel);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.Ok(result.Model);
    }
}