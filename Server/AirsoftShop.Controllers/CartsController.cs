namespace AirsoftShop.Controllers;

using Attributes;
using Common.Constants;
using Common.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Carts;
using Services.Models.Cart;
using Services.Services.Cart;
using static Common.Constants.Messages;
using static Common.Constants.Constants.ControllerRoutes;

[Authorize]
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

    [HttpPost]
    [ValidateClient]
    public async Task<IActionResult> Add([FromBody] CartInputModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        var result = await this.cartService.Add(user.ClientId, model.ItemId!);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.CreatedAtAction(nameof(this.Add), result.Model);
    }
    
    [HttpPost]
    [Route(Constants.ControllerRoutes.BulkAdd)]
    [ValidateClient]
    public async Task<IActionResult> BulkAdd([FromBody] BulkCartInputModel model)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        var result = await this.cartService.Add(user.ClientId, model.ItemIds!);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }

        return this.CreatedAtAction(nameof(this.Add), result.Model);
    }

    [HttpGet]
    [ValidateClient]
    public async Task<IActionResult> GetUserItemsInCart()
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        var items = await this.cartService.GetItemsInCart(user.ClientId);

        return this.Ok(items);
    }

    [HttpDelete]
    [ValidateClient]
    public async Task<IActionResult> DeleteItemById(string itemId)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);

        var result = await this.cartService.DeleteItemById(user.ClientId, itemId);
        if (!result)
        {
            return this.BadRequest(new { ErrorMessage = UnsuccessfulActionMsg });
        }

        return this.Ok(new { Message = SuccessfulDeleteMsg });
    }
    
    [HttpGet]
    [Route(DeliveryData)]
    public async Task<IActionResult> GetDeliveryData()
    {
        var data = await this.cartService.GetCartDeliveryData();

        return this.Ok(data);
    }
    
    [HttpGet]
    [Route(GetNavData)]
    [AllowAnonymous]
    public async Task<IActionResult> GetProductCountAndPrice()
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);
        
        if (user?.ClientId is null)
        {
            var cartModel = new NavCartServiceModel()
            {
                ItemsCount = 0,
                TotalPrice = 0
            };

            return this.Ok(cartModel); 
        }

        var result = await this.cartService.GetCartData(user.ClientId);

        return this.Ok(result);
    }
}