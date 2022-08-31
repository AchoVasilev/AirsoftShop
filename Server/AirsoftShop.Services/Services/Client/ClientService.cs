namespace AirsoftShop.Services.Services.Client;

using Common.Models;
using Data.Models;
using Data.Models.Images;
using Data.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Address;
using Models.City;
using Models.Client;
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
                PhoneNumber = model.PhoneNumber,
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
    
    public async Task<OperationResult<UserClientServiceModel>> Profile(string userId)
    {
        var user = await this.userManager
            .Users
            .Include(x => x.Image)
            .Include(x => x.Client)
            .ThenInclude(x => x.Address)
            .ThenInclude(x => x.City)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user is null)
        {
            return UserNotDealerMsg;
        }

        var model = new UserClientServiceModel()
        {
            UserId = user.Id,
            ClientId = user.DealerId,
            Email = user.Email,
            Username = user.UserName,
            ImageUrl = user.Image.Url,
            Client = new ClientServiceModel()
            {
                FirstName = user.Client.FirstName,
                LastName = user.Client.LastName,
                PhoneNumber = user.Client.PhoneNumber,
                Address = new AddressServiceModel()
                {
                    StreetName = user.Client.Address.StreetName,
                    City = new CityServiceModel()
                    {
                        Id = user.Client.Address.CityId,
                        Name = user.Client.Address.City.Name,
                        ZipCode = user.Client.Address.City.ZipCode
                    }
                }
            }
        };

        return model;
    }

    public async Task<OperationResult<ClientResultServiceModel>> EditClient(string? userId, ClientEditServiceModel editModel)
    {
        var city = await this.data.Cities
            .FirstOrDefaultAsync(x => x.Name == editModel.CityName);

        if (city is null)
        {
            return InvalidCityMsg;
        }
        
        var user = await this.data.Users
            .Where(x => x.Id == userId && x.Email == editModel.Email)
            .Include(x => x.Client)
            .ThenInclude(x => x.Address)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return NotAuthorizedMsg;
        }
        
        user.Client.Address.StreetName = editModel.StreetName;
        user.Client.Address.CityId = city.Id;
        user.Client.FirstName = editModel.FirstName;
        user.Client.LastName = editModel.LastName;
        user.Email = editModel.Email;
        user.Client.Email = editModel.Email;
        user.Client.PhoneNumber = editModel.PhoneNumber;

        await this.data.SaveChangesAsync();

        return true;
    }
}