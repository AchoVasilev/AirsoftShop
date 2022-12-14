namespace AirsoftShop.Controllers;

using Attributes;
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
using static Common.Constants.Constants.ControllerRoutes;
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
    [ValidateDealer]
    public async Task<ActionResult> Create([FromForm] CreateFieldModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        var fileModels = new List<IFileServiceModel>();
        foreach (var image in model.Images)
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
            Images = fileModels,
            Description = model.Description
        };

        var result = await this.fieldService.Create(user.DealerId, serviceModel);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.Ok(result.Model);
    }

    [HttpGet]
    [Route(ById)]
    public async Task<ActionResult> Details(int id)
    {
        var result = await this.fieldService.Details(id);
        if (result.Failed)
        {
            return this.BadRequest(result.ErrorMessage);
        }

        return this.Ok(result.Model);
    }

    [HttpPut]
    [ValidateDealer]
    public async Task<ActionResult> Edit(FieldEditModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        if (user.DealerId != model.DealerId)
        {
            return this.Unauthorized(new { ErrorMessage = NotAuthorizedMsg });
        }

        var serviceModel = new EditFieldServiceModel()
        {
            Id = model.Id,
            Description = model.Description,
            CityId = model.CityId,
            DealerId = model.DealerId,
            StreetName = model.StreetName,
            ZipCode = model.ZipCode
        };

        var result = await this.fieldService.Edit(serviceModel);
        if (result.Failed)
        {
            return this.BadRequest(result.ErrorMessage);
        }

        return this.Ok();
    }

    [HttpDelete]
    [Route(ById)]
    [ValidateDealer]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        var result = await this.fieldService.Delete(id, user.DealerId);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.Ok();
    }
}