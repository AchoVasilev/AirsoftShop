namespace AirsoftShop.Controllers;

using Attributes;
using Common.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.WishLists;
using Services.Services.WishList;

[Authorize]
public class WishListsController : BaseController
{
    private readonly IWishListService wishListService;
    private readonly ICurrentUserService currentUserService;
    private readonly UserManager<ApplicationUser> userManager;
    
    public WishListsController(
        IWishListService wishListService, 
        ICurrentUserService currentUserService, 
        UserManager<ApplicationUser> userManager)
    {
        this.wishListService = wishListService;
        this.currentUserService = currentUserService;
        this.userManager = userManager;
    }

    [HttpGet]
    [ValidateClient]
    public async Task<ActionResult> All()
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        var items = await this.wishListService.GetItems(user.ClientId);

        return this.Ok(items);
    }

    [HttpPost]
    [ValidateClient]
    public async Task<ActionResult> Add([FromBody]AddItemToWishListModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        var result = await this.wishListService.Add(model.Id!, user.ClientId);
        if (result.Failed)
        {
            return this.BadRequest(result.ErrorMessage);
        }

        return this.NoContent();
    }

    [HttpDelete]
    [ValidateClient]
    public async Task<ActionResult> Remove(string id)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);
        
        var result = await this.wishListService.Remove(user.ClientId, id);
        if (result.Failed)
        {
            return this.BadRequest(result.ErrorMessage);
        }

        return this.NoContent();
    }

    [HttpDelete]
    [Route(nameof(BulkRemove))]
    [ValidateClient]
    public async Task<ActionResult> BulkRemove([FromBody]BulkRemoveItemsFromWishListModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        var result = await this.wishListService.Remove(user.ClientId, model.Ids!);
        if (result.Failed)
        {
            return this.BadRequest(result.ErrorMessage);
        }

        return this.NoContent();
    }
}