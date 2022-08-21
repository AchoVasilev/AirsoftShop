namespace AirsoftShop.Controllers;

using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Clients;
using Services.Models.Clients;
using Services.Services.Client;
using static Common.Constants.Messages;

public class ClientsController : BaseController
{
    private readonly IClientService clientService;
    private readonly UserManager<ApplicationUser> userManager;

    public ClientsController(IClientService clientService, UserManager<ApplicationUser> userManager)
    {
        this.clientService = clientService;
        this.userManager = userManager;
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
            Phone = model.Phone,
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
}