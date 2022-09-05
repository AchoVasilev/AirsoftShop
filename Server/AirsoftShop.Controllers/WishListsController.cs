namespace AirsoftShop.Controllers;

using Common.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.WishLists;
using Services.Services.WishList;
using static Common.Constants.Messages;

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
    public async Task<ActionResult> All()
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);
        if (user?.ClientId is null)
        {
            return this.BadRequest(new { ErrorMessage = UserNotClientMsg });
        }

        var items = await this.wishListService.GetItems(user.ClientId);

        return this.Ok(items);
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody]AddItemToWishListModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);
        if (user?.ClientId is null)
        {
            return this.BadRequest(new { ErrorMessage = UserNotClientMsg });
        }

        var result = await this.wishListService.Add(model.Id, user.ClientId);
        if (result.Failed)
        {
            return this.BadRequest(result.ErrorMessage);
        }

        return this.NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Remove(string id)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);
        if (user?.ClientId is null)
        {
            return this.BadRequest(new { ErrorMessage = UserNotClientMsg });
        }
        
        var result = await this.wishListService.Remove(user.ClientId, id);
        if (result.Failed)
        {
            return this.BadRequest(result.ErrorMessage);
        }

        return this.NoContent();
    }
}