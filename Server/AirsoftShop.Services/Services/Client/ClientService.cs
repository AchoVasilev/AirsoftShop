namespace AirsoftShop.Services.Services.Client;

using Common.Models;
using Data.Models;
using Data.Models.Images;
using Data.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Clients;
using static Common.Constants.Messages;

public class ClientService : IClientService
{
    private readonly ApplicationDbContext data;
    private readonly UserManager<ApplicationUser> userManager;

    public ClientService(ApplicationDbContext data, UserManager<ApplicationUser> userManager)
    {
        this.data = data;
        this.userManager = userManager;
    }

    public async Task<OperationResult<ClientResultServiceModel>> CreateClient(CreateClientServiceModel model)
    {
        var city = await this.data.Cities
            .FirstOrDefaultAsync(x => x.Name == model.CityName);

        if (city is null)
        {
            return InvalidCityMsg;
        }
        
        var applicationUser = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.Username,
            Image = new Image
            {
                Url = "https://res.cloudinary.com/dpo3vbxnl/image/upload/v1649172192/BgAirsoft/NoAvatarProfileImage_uj0zyg.png",
                Extension = "png",
                Name = "NoAvatarProfileImage"
            },
            Client = new Client
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.Phone,
                Address = new Address
                {
                    StreetName = model.StreetName,
                    CityId = city.Id
                }
            }
        };

        var result = await this.userManager.CreateAsync(applicationUser, model.Password);

        if (result.Succeeded)
        {
            await this.data.SaveChangesAsync();
            var dealerResult = new ClientResultServiceModel()
            {
                UserId = applicationUser.Id,
                ClientId = applicationUser.ClientId,
                UserName = applicationUser.UserName
            };
            
            return dealerResult;
        }

        return result.Errors
            .Select(error => error.Description)
            .ToList();
    }
}