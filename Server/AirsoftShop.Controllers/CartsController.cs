namespace AirsoftShop.Controllers;

using Common.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Cart;
using static Common.Constants.Messages;

public class CartsController : BaseController
{
    private readonly ICurrentUserService currentUserService;
    private readonly ICartService cartService;
    private readonly UserManager<ApplicationUser> userManager;

    public CartsController(
        ICurrentUserService currentUserService,
        ICartService cartService,
        UserManager<ApplicationUser> userManager)
    {
        this.currentUserService = currentUserService;
        this.cartService = cartService;
        this.userManager = userManager;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] string gunId)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);
        if (user?.ClientId is null)
        {
            return this.Unauthorized(new { ErrorMessage = NotAuthorizedMsg });
        }

        var result = await this.cartService.Add(user.ClientId, gunId);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.CreatedAtAction(nameof(this.Add), result.Model);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUserItemsInCart()
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);
        if (user?.ClientId is null)
        {
            return this.Unauthorized(new { ErrorMessage = NotAuthorizedMsg });
        }

        var items = await this.cartService.GetItemsInCart(user.ClientId);

        return this.Ok(items);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteItemById(string itemId)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);
        if (user?.ClientId is null)
        {
            return this.Unauthorized(new { ErrorMessage = NotAuthorizedMsg });
        }

        var result = await this.cartService.DeleteItemById(user.ClientId, itemId);
        if (!result)
        {
            return this.BadRequest(new { ErrorMessage = UnsuccessfulActionMsg });
        }

        return this.Ok(new { Message = SuccessfulDeleteMsg });
    }
}