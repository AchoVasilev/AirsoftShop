namespace AirsoftShop.Controllers;

using Common.Models;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.Users;
using Services.Services.Identity;

public class UsersController : BaseController
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IIdentityService identityService;
    private readonly IOptions<JwtConfiguration> options;

    public UsersController(
        UserManager<ApplicationUser> userManager, 
        IIdentityService identityService,
        IOptions<JwtConfiguration> options)
    {
        this.userManager = userManager;
        this.identityService = identityService;
        this.options = options;
    }

    [HttpPost]
    [Route(nameof(Login))]
    public async Task<ActionResult<LoginResponseModel>> Login(LoginUserRequestModel model)
    {
        var user = await this.userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            return this.Unauthorized();
        }

        var validPassword = await this.userManager.CheckPasswordAsync(user, model.Password);
        if (!validPassword)
        {
            return this.Unauthorized();
        }

        var appSettings = this.options.Value.Secret;
        var encryptedToken = this.identityService.GenerateJwtToken(user.Id, user.Email, appSettings);

        return new LoginResponseModel() { Token = encryptedToken, IsClient = user.ClientId != null};
    }
}