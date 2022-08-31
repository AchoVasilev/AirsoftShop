namespace AirsoftShop.Controllers;

using Common.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Clients;
using Services.Models.Client;
using Services.Services.Client;
using static Common.Constants.Messages;

public class ClientsController : BaseController
{
    private readonly IClientService clientService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ICurrentUserService currentUserService;

    public ClientsController(
        IClientService clientService, 
        UserManager<ApplicationUser> userManager,
        ICurrentUserService currentUserService)
    {
        this.clientService = clientService;
        this.userManager = userManager;
        this.currentUserService = currentUserService;
    }

    [HttpPost]
    public async Task<ActionResult> Register(ClientInputModel model)
    {
        var user = await this.userManager.FindByNameAsync(model.Username);
        if (user != null)
        {
            return this.BadRequest(new { ErrorMessage = UsernameExistsMsg });
        }

        var client = new CreateClientServiceModel()
        {
            CityName = model.CityName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Password = model.Password,
            PhoneNumber = model.PhoneNumber,
            StreetName = model.StreetName,
            Username = model.Username
        };

        var result = await this.clientService.CreateClient(client);
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
        
        var result = await this.clientService.Profile(userId!);
        if (result.Failed)
        {
            return this.BadRequest(new { result.ErrorMessage });
        }
        
        return this.Ok(result.Model);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<ActionResult> Edit(EditClientFormModel model)
    {
        var user = await this.userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            return this.BadRequest(new { ErrorMessage = UserDoesNotExistMsg });
        }
        
        var userId = this.currentUserService.GetUserId();
        var editModel = new ClientEditServiceModel()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            CityName = model.CityName,
            StreetName = model.StreetName
        };
        
        var result = await this.clientService.EditClient(userId, editModel);
        if (result.Succeeded)
        {
            return this.Ok(new { Message = SuccessfulEditMsg });
        }

        return this.BadRequest(new { ErrorMessage = UnsuccessfulActionMsg });
    }
}