namespace AirsoftShop.Controllers.Attributes;

using Common.Services;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static Common.Constants.Messages;

internal class ValidateClientAttribute : ActionFilterAttribute
{
    private ICurrentUserService? currentUserService;
    private UserManager<ApplicationUser>? userManager;
    
    public override async void OnActionExecuting(ActionExecutingContext context)
    {
        this.currentUserService = ValidationAttributesHelper.RequestServiceContext<ICurrentUserService>(context);
        this.userManager = ValidationAttributesHelper.RequestServiceContext<UserManager<ApplicationUser>>(context);
        
        var userId = this.currentUserService!.GetUserId();
        if (userId is null)
        {
            context.Result = new UnauthorizedObjectResult(new { ErrorMessage = UserNotLoggedInMsg });
        }
        
        var user = await this.userManager!.FindByIdAsync(userId);
        
        if (user.ClientId is null)
        {
            context.Result = new UnauthorizedObjectResult(new { ErrorMessage = UserNotClientMsg });
        }
    }
}