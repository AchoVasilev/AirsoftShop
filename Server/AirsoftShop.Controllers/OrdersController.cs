namespace AirsoftShop.Controllers;

using Common.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Orders;
using Services.Models.Order;
using Services.Services.Cart;
using Services.Services.Order;

using static Common.Constants.Messages;
[Authorize]
public class OrdersController : BaseController
{
    private readonly IOrderService orderService;
    private readonly ICartService cartService;
    private readonly ICurrentUserService currentUserService;
    private readonly UserManager<ApplicationUser> userManager;

    public OrdersController(
        IOrderService orderService, 
        ICartService cartService, 
        ICurrentUserService currentUserService, 
        UserManager<ApplicationUser> userManager)
    {
        this.orderService = orderService;
        this.cartService = cartService;
        this.currentUserService = currentUserService;
        this.userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderInputModel order)
    {
        var userId = this.currentUserService.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId);
        if (user?.ClientId is null)
        {
            return this.BadRequest(new { ErrorMessage = UserNotClientMsg });
        }

        var model = new CreateOrderServiceModel()
        {
            CourierId = order.CourierId,
            PaymentType = order.PaymentType,
            TotalPrice = order.TotalPrice,
            GunsIds = order.GunsIds
        };
        
        var createdResult = await this.orderService.CreateOrder(user.ClientId, model);
        if (createdResult.Failed)
        {
            return this.BadRequest(new { createdResult.ErrorMessage });
        }

        var cartIsCleared = await this.cartService.ClearCart(user.ClientId);
        if (cartIsCleared == false)
        {
            return this.BadRequest(new { ErrorMessage = UnsuccessfulActionMsg });
        }

        return this.Ok(new { Message = SuccessfulOrderMsg, createdResult.Model.OrdersCount });
    }
}